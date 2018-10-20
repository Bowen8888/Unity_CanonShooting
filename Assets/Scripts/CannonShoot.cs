using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShoot : MonoBehaviour
{

	public GameObject cannonBall;

	public float firePower;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			ShootCannon();
		}

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			MoveCannon(true);
		}

		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			MoveCannon(false);
		}
	}

	public void ShootCannon()
	{
		GameObject thisCannonBall = Instantiate(cannonBall, transform.position, transform.rotation);
		thisCannonBall.GetComponent<Rigidbody>().AddRelativeForce(firePower,0,0, ForceMode.Impulse);
	}

	public void MoveCannon(bool up)
	{
		if (up)
		{
			transform.parent.transform.Rotate(0,0,10f);
		}
		else
		{
			transform.parent.transform.Rotate(0,0,-10f);
		}
	}
}
