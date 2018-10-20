using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainGenerator : MonoBehaviour
{

	public GameObject dirtPrefab;
	public GameObject grassPrefab;

	private int minX = -30;
	private int maxX = 30;
	private int minY = -30;
	private int maxY = 30;

	private PerlinNoise noise;
	
	// Use this for initialization
	void Start () {
		noise = new PerlinNoise(Random.Range(1000000,10000000));
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
				Instantiate(block, new Vector2(i * width, j * height), Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
