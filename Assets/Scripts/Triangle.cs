using UnityEngine;

public class Triangle : Shape
{
	public override void CalculateShape()
	{
		vertices = new Vector3[3];

		vertices[0] = new Vector3(-1f, -1f);
		vertices[1] = new Vector3(0f, 1);
		vertices[2] = new Vector3(1, -1);

		triangles = new int[] { 0, 1, 2 };
	}
}
