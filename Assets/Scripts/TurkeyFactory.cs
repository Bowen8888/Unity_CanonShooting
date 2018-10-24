using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurkeyFactory : MonoBehaviour
{

	private static int turkeyAmount = 0;
	public GameObject turkeyPrefab;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (turkeyAmount < 1)
		{
			turkeyAmount++;
			Instantiate(turkeyPrefab);
		}
	}

	public static void DecrementTurkeyAmount()
	{
		turkeyAmount--;
	}
}
