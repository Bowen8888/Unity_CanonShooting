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
		_turkey.UpdateTurkey();
		RenderPoints();
	}

	private void RenderPoints()
	{
		for (int i = 0; i < TurkeyPoints.Count; i++)
		{
			TurkeyPoints[i].transform.localPosition = new Vector3(_turkey.Points[i].x,_turkey.Points[i].y,-0.01f);
		}
	}


}
