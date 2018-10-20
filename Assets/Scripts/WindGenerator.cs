﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGenerator : MonoBehaviour
{

	private int[] windMagnitudes = new int[]{-20,-15,-10,-5,0,5,10,15,20};
	public int currentWind = 0;
	
	private float time = 0.0f;
	public float interpolationPeriod = 0.5f;
	
	// Use this for initialization
	void Start () {
		UpdateWind();
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
 
		if (time >= interpolationPeriod) {
			time = 0.0f;
			UpdateWind();
		}
	}

	private void UpdateWind()
	{
		System.Random rnd = new System.Random();
		int index = rnd.Next(windMagnitudes.Length);
		currentWind = windMagnitudes[index];
	}
}
