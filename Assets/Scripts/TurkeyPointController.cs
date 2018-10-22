using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurkeyPointController : MonoBehaviour {

	Turkey _turkey ;

	public List<GameObject> TurkeyPoints = new List<GameObject>();
	// Use this for initialization
	void Start () {
		List<Point> points = new List<Point>();
		foreach (var obj in TurkeyPoints)
		{
			float xPosition = obj.transform.localPosition.x;
			float yPosition = obj.transform.localPosition.y;
			points.Add(new Point(xPosition,yPosition));
		}
		_turkey = new Turkey(points);
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
	}

	private void RenderPoints()
	{
		for (int i = 0; i < TurkeyPoints.Count; i++)
		{
			TurkeyPoints[i].transform.localPosition = new Vector3(_turkey.Points[i].x,_turkey.Points[i].y,-0.01f);
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
