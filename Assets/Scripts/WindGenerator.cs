using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGenerator : MonoBehaviour
{
	private float time = 0.0f;
	public float interpolationPeriod = 0.5f;
	
	// Use this for initialization
	void Start () {
		Wind.UpdateWind();
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
 
		if (time >= interpolationPeriod) {
			time = 0.0f;
			Wind.UpdateWind();
		}
	}


}
