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
	private float weight = 9;
	private Dictionary<int,float> mountainTops;
	private float mountainTop;
	
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
		Vector2 actingForce = new Vector2(0,-5);
		if ( yPosition< groundHeight || xPosition > screenRightLimit)
		{
			Destroy(gameObject);
		}

		if (mountainTop != null && yPosition > mountainTop)
		{
			actingForce = new Vector2(Wind.currentWind*0.5f,-5);
		}
		
		float ySpeed = gameObject.GetComponent<Rigidbody>().velocity.y;
		
		if (xPosition < leftWallBorder && xSpeed < 0)
		{
			gameObject.GetComponent<Rigidbody>().velocity = new Vector2(-(xSpeed*0.6f),gameObject.GetComponent<Rigidbody>().velocity.y);
		}

		int roundedXPosition = (int) Math.Round(xPosition);
		
		if (mountainTops!= null && mountainTops.ContainsKey(roundedXPosition) && yPosition < mountainTops[roundedXPosition])
		{
			if(Math.Abs(xSpeed) < 1 || Math.Abs(ySpeed) < 1)
			{
				Destroy(gameObject);
			}

			if (ySpeed < 0)
			{
				transform.position = new Vector2(roundedXPosition, mountainTops[roundedXPosition]);
			}
			gameObject.GetComponent<Rigidbody>().velocity = new Vector2(-(xSpeed*0.5f),
				-(ySpeed)*0.75f);
		}
		else
		{
			velocity += weight * actingForce * Time.deltaTime;
			Vector2 deltaPosition = velocity * Time.deltaTime;
			Vector2 v = gameObject.GetComponent<Rigidbody>().velocity;
			gameObject.GetComponent<Rigidbody>().velocity =  v + deltaPosition;
		}
		
	}

	public void SetMountainTops(Dictionary<int,float> dictionary)
	{
		mountainTops = dictionary;
	}

	public void SetMountainTop(float mountainTop)
	{
		this.mountainTop = mountainTop;
	}
}
