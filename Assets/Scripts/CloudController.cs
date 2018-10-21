using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
	private float leftBorder = -19.27f;
	private float rightBorder = 20.25f;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.position.x < leftBorder || transform.position.x > rightBorder)
		{
			transform.position = new Vector3(0,transform.position.y,0);
		}
		
		int wind = Wind.currentWind;
		Vector2 movingVector = new Vector2(wind,0);
		gameObject.GetComponent<Rigidbody>().velocity =  movingVector;
	}
}
