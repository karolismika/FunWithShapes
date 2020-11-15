using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octagon : Shape
{
	public override void CalculateShape()
	{
		vertices = new Vector3[9];

		vertices[0] = new Vector3(0f, 0f);
		vertices[1] = new Vector3(-0.4f, -1f);
		vertices[2] = new Vector3(-1f, -0.4f);
		vertices[3] = new Vector3(-1f, 0.4f);
		vertices[4] = new Vector3(-0.4f, 1f);
		vertices[5] = new Vector3(0.4f, 1f);
		vertices[6] = new Vector3(1f, 0.4f);
		vertices[7] = new Vector3(1f, -0.4f);
		vertices[8] = new Vector3(0.4f, -1f);

		triangles = new int[] { 0, 1, 2, 0, 2, 3, 0, 3, 4, 0, 4, 5, 0, 5, 6, 0, 6, 7, 0, 7, 8, 0, 8, 1 };
	}
}
