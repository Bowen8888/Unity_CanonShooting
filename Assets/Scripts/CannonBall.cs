using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
	private float groundHeight = -7.711f;
	private float screenLeftLimit = -20.163f;
	private float screenRightLimit = 20.06f;
	private Vector2 velocity;
	private float weight = 40;
	
	// Use this for initialization
	void Start ()
	{
		velocity = gameObject.GetComponent<Rigidbody>().velocity;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < groundHeight || transform.position.x < screenLeftLimit 
		                                        || transform.position.x > screenRightLimit)
		{
			Destroy(gameObject);
		}

		velocity += weight * Physics2D.gravity * Time.deltaTime;
		Vector2 deltaPosition = velocity * Time.deltaTime;
		Vector2 v = gameObject.GetComponent<Rigidbody>().velocity;
		gameObject.GetComponent<Rigidbody>().velocity =  v + deltaPosition;
	}
}
