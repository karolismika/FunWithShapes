using UnityEngine;

public class Square : Shape
{
	public override void CalculateShape()
	{
		vertices = new Vector3[4];

		vertices[0] = new Vector3(-1f, -1f);
		vertices[1] = new Vector3(-1f, 1f);
		vertices[2] = new Vector3(1f, 1f);
		vertices[3] = new Vector3(1f, -1f);

		triangles = new int[] { 0, 1, 2, 0, 2, 3 };
	}
}
