using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind {
	private static int[] windMagnitudes = new int[]{-5,-4,-3,-2,-1,0,1,2,3,4,5};
	public static int currentWind = 0;
	
	public static void UpdateWind()
	{
		System.Random rnd = new System.Random();
		int index = rnd.Next(windMagnitudes.Length);
		currentWind = windMagnitudes[index];
	}
	
}
