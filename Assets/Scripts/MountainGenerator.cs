using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MountainGenerator : MonoBehaviour
{

	public GameObject dirtPrefab;
	public GameObject grassPrefab;

	private int minX = -90;
	private int maxX = 85;
	private int minY = -90;
	private int maxY = 140;

	private PerlinNoise noise;
	
	private static Dictionary<int,float> mountainTops;
	private static float mountainTop;
	
	// Use this for initialization
	void Start () {
		noise = new PerlinNoise(Random.Range(1000000,10000000));
		mountainTops = new Dictionary<int, float>();
		mountainTop = minY;
		Regenerate();
		
	}

	private void Regenerate()
	{
		float width = dirtPrefab.transform.lossyScale.x;
		float height = dirtPrefab.transform.lossyScale.y;
		
		
		for (int i = minX; i < maxX; i++)
		{
			int columnHeight = 2 + noise.getNoise(i - minX, maxY - minY - 2, Math.Abs(minX)+Math.Abs(maxX)-1);
			for (int j = minY; j < columnHeight + minY; j++)
			{
				GameObject block = (j == minY + columnHeight - 1) ? grassPrefab : dirtPrefab;
				
				GameObject cell = Instantiate(block, new Vector2(i * width, j * height), Quaternion.identity);
				if (j == minY + columnHeight - 1)
				{
					float yCoord = cell.transform.position.y + 0.5f;
					mountainTops[(int)cell.transform.position.x] = yCoord;
					mountainTop = Math.Max(mountainTop, yCoord);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R))
		{
			GameObject[] cells = GameObject.FindGameObjectsWithTag("Cell");
			for (int i=0; i< cells.Length;i++)
			{
				Destroy(cells[i]);
			}
			Regenerate();
		}
	}

	public static Dictionary<int,float> GetMountainTops()
	{
		return new Dictionary<int, float>(mountainTops);
	}

	public static float GetMountainTop()
	{
		return mountainTop;
	}
}
