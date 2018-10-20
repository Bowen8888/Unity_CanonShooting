using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
	public GameObject windController;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
	{
		int wind = windController.GetComponent<WindGenerator>().currentWind;
		Vector2 movingVector = new Vector2(wind,0);
		gameObject.GetComponent<Rigidbody>().velocity =  movingVector;
	}
}
