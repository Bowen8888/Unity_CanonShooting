using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurkeyPointController : MonoBehaviour {

	Turkey _turkey ;
	private float time = 0.0f;
	private float interpolationPeriod = 3f;
	private float jumpTimer = 0.0f;
	private float jumpPeriod;

	public List<GameObject> TurkeyPoints = new List<GameObject>();
	// Use this for initialization
	void Start () {
		System.Random rnd = new System.Random();
		List<Point> points = new List<Point>();
		float offset = (float) (rnd.NextDouble() * 5);
		foreach (var obj in TurkeyPoints)
		{
			float xPosition = obj.transform.position.x;
			float yPosition = obj.transform.position.y;
			points.Add(new Point(xPosition-offset,yPosition));
		}
		_turkey = new Turkey(points);
		jumpPeriod = (float) (rnd.NextDouble()* 5f+5);
		TurkeyLateralMove();
	}
	
	// Update is called once per frame
	void Update () {
		if (_turkey.minX > 8.04)
		{
			Destroy(gameObject);
			TurkeyFactory.DecrementTurkeyAmount();
		}

		jumpTimer += Time.deltaTime;
		if (_turkey.grounded && jumpTimer>jumpPeriod)
		{
			jumpTimer = 0.0f;
			System.Random rnd = new System.Random();
			if (rnd.NextDouble() < 0.6)
			{
				_turkey.TurkeyJump();
			}
		}
		
		if (Input.GetKeyDown(KeyCode.S))
		{
			_turkey.SlightJump();
		}

		float wf = 0;
		if (_turkey.maxX > -8.77)
		{
			wf = -0.008f;
		}

		if (_turkey.minY > MountainGenerator.GetMountainTop())
		{
			_turkey.UpdateTurkey(Wind.currentWind*0.02f,wf);
		}
		else
		{
			_turkey.UpdateTurkey(0,wf);
		}
		
		RenderPoints();
		RenderLines();
		time += Time.deltaTime;

		CannonBallCollisionDetection();
		if (time >= interpolationPeriod) {
			time = 0.0f;
			if (_turkey.grounded && _turkey.maxX < -8.77)
			{
				TurkeyLateralMove();
			}
		}
		MountainCollisionDetection();
	}

	private void TurkeyLateralMove()
	{
		System.Random rnd = new System.Random();
		_turkey.LateralSlide(rnd.NextDouble() < 0.5);
	}

	private void RenderPoints()
	{
		for (int i = 0; i < TurkeyPoints.Count; i++)
		{
			TurkeyPoints[i].transform.position = new Vector3(_turkey.Points[i].x,_turkey.Points[i].y,-0.01f);
		}
	}

	private void RenderLines()
	{
		foreach (var lineController in transform.GetComponentsInChildren<LineController>())
		{
			lineController.DrawLine();	
		}		
	}

	private void CannonBallCollisionDetection()
	{
		GameObject[] cannonBalls = GameObject.FindGameObjectsWithTag("CannonBall");

		for (int i=0; i<cannonBalls.Length; i++)
		{
			Vector2 ballCoord = cannonBalls[i].transform.position;
			for (int j=0; j< TurkeyPoints.Count; j++)
			{
				Vector2 turkeyCoord = TurkeyPoints[j].transform.position;
				if (distance(ballCoord,turkeyCoord) < 0.5)
				{
					Vector2 ballVelocity = cannonBalls[i].GetComponent<Rigidbody>().velocity*0.05f;
					_turkey.VertexSlide(j,ballVelocity.x,ballVelocity.y);
					Destroy(cannonBalls[i]);
					break;
				}
			}
		}
	}

	private void MountainCollisionDetection()
	{
		Dictionary<int, float> mountainTops = MountainGenerator.GetMountainTops();

		float xminPosition = _turkey.minX;
		float xmaxPosition = (_turkey.minX + _turkey.maxX) /2;
		float yPosition = _turkey.minY;
		int roundedXminPosition = (int) Math.Round(xminPosition);
		int roundedXmaxPosition = (int) Math.Round(xmaxPosition);
		if (mountainTops.Keys.Contains(roundedXminPosition) && yPosition < mountainTops[roundedXminPosition])
		{
			_turkey.MountainBouncing(mountainTops[roundedXminPosition]);
		}
		else if ((mountainTops.Keys.Contains(roundedXmaxPosition) && yPosition < mountainTops[roundedXmaxPosition]))
		{
			_turkey.MountainBouncing(mountainTops[roundedXmaxPosition]);
		}
	}

	private float distance(Vector2 p0, Vector2 p1)
	{
		float dx = p1.x - p0.x;
		float dy = p1.y - p0.y;
		return (float) Math.Sqrt(dx * dx + dy * dy);
	}
}
