using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurkeyPointController : MonoBehaviour {

	Turkey _turkey ;
	private float time = 0.0f;
	public float interpolationPeriod = 5f;

	public List<GameObject> TurkeyPoints = new List<GameObject>();
	// Use this for initialization
	void Start () {
		List<Point> points = new List<Point>();
		foreach (var obj in TurkeyPoints)
		{
			float xPosition = obj.transform.position.x;
			float yPosition = obj.transform.position.y;
			points.Add(new Point(xPosition,yPosition));
		}
		_turkey = new Turkey(points);
		TurkeyLateralMove();
	}
	
	// Update is called once per frame
	void Update () {
		if (_turkey.grounded && Input.GetKeyDown(KeyCode.J))
		{
			_turkey.TurkeyJump();
		}
		_turkey.UpdateTurkey();
		RenderPoints();
		RenderLines();
		time += Time.deltaTime;
 
		if (time >= interpolationPeriod) {
			time = 0.0f;
			if (_turkey.grounded)
			{
				TurkeyLateralMove();
			}
		}
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


}
