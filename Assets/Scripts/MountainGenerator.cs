using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MountainGenerator : MonoBehaviour
{

	public GameObject dirtPrefab;
	public GameObject grassPrefab;

	private int minX = -30;
	private int maxX = 30;
	private int minY = -30;
	private int maxY = 30;

	private PerlinNoise noise;
	
	private Dictionary<int,float> mountainTops;
	
	// Use this for initialization
	void Start () {
		noise = new PerlinNoise(Random.Range(1000000,10000000));
		mountainTops = new Dictionary<int, float>();
		Regenerate();
		
	}

	private void Regenerate()
	{
		float width = dirtPrefab.transform.lossyScale.x;
		float height = dirtPrefab.transform.lossyScale.y;
		
		
		for (int i = minX; i < maxX; i++)
		{
			int columnHeight = 2 + noise.getNoise(i - minX, maxY - minY - 2);
			for (int j = minY; j < columnHeight + minY; j++)
			{
				GameObject block = (j == minY + columnHeight - 1) ? grassPrefab : dirtPrefab;
				
				GameObject cell = Instantiate(block, new Vector2(i * width, j * height), Quaternion.identity);
				
				if (j == minY + columnHeight - 1)
				{
					mountainTops[(int)cell.transform.position.x] = cell.transform.position.y+ 0.5f;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Dictionary<int,float> GetMountainTops()
	{
		return new Dictionary<int, float>(mountainTops);
	}
}
