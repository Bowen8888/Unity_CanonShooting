using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
	public GameObject end;
	private LineRenderer _lineRenderer;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		_lineRenderer = GetComponent<LineRenderer>();
		Vector3[] vector3s = new Vector3[]{transform.position,end.transform.position};
		_lineRenderer.SetPositions(vector3s);
	}
}
