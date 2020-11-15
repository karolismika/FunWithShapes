using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapezoid : Shape
{
	public override void CalculateShape()
	{
		vertices = new Vector3[6];

		vertices[0] = new Vector3(-1f, -1f);
		vertices[1] = new Vector3(-1.5f, -1f);
		vertices[2] = new Vector3(-1f, 1f);
		vertices[3] = new Vector3(1f, 1f);
		vertices[4] = new Vector3(1.5f, -1f);
		vertices[5] = new Vector3(1f, -1f);

		triangles = new int[] { 0, 1, 2, 0, 2, 3, 0, 3, 5, 3, 4, 5 };
	}
}
