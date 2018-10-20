using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
	private float groundHeight = -7.711f;
	private float leftWallBorder = -18f;
	private float screenRightLimit = 20.06f;
	private Vector2 velocity;
	private float weight = 35;
	private Dictionary<int,float> mountainTops;
	
	// Use this for initialization
	void Start ()
	{
		velocity = gameObject.GetComponent<Rigidbody>().velocity;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float xPosition = transform.position.x;
		float yPosition = transform.position.y;
		float xSpeed = gameObject.GetComponent<Rigidbody>().velocity.x;
		if ( yPosition< groundHeight || xPosition > screenRightLimit)
		{
			Destroy(gameObject);
		}
		
		float ySpeed = gameObject.GetComponent<Rigidbody>().velocity.y;
		
		if (xPosition < leftWallBorder && xSpeed < 0)
		{
			gameObject.GetComponent<Rigidbody>().velocity = new Vector2(-(xSpeed*0.6f),gameObject.GetComponent<Rigidbody>().velocity.y);
		}

		if (mountainTops!= null && mountainTops.ContainsKey((int) xPosition) && yPosition < mountainTops[(int) xPosition] && ySpeed < 0)
		{
			if(Math.Abs(xSpeed) < 1 || Math.Abs(ySpeed) < 1)
			{
				Destroy(gameObject);
			}
			gameObject.GetComponent<Rigidbody>().velocity = new Vector2(-(xSpeed*0.6f),
				-(ySpeed)*0.6f);
		}
		else
		{
			velocity += weight * Physics2D.gravity * Time.deltaTime;
			Vector2 deltaPosition = velocity * Time.deltaTime;
			Vector2 v = gameObject.GetComponent<Rigidbody>().velocity;
			gameObject.GetComponent<Rigidbody>().velocity =  v + deltaPosition;
		}
		
	}

	public void SetMountainTops(Dictionary<int,float> dictionary)
	{
		mountainTops = dictionary;
	}
}
