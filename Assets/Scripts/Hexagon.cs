using UnityEngine;

public class Hexagon : Shape
{
	public override void CalculateShape()
	{
		vertices = new Vector3[7];
		float triangleSide = 1.15f;
		float equilateralTriangleAltitude = 1f / 2f * Mathf.Sqrt(3f) * triangleSide;

		vertices[0] = new Vector3(0f, 0f); //center
		vertices[1] = new Vector3(0f, -triangleSide); //bottom point
		vertices[2] = new Vector3(-equilateralTriangleAltitude, -(triangleSide / 2f)); //bottom left point
		vertices[3] = new Vector3(-equilateralTriangleAltitude, triangleSide / 2f); //top left point
		vertices[4] = new Vector3(0f, triangleSide); //top point
		vertices[5] = new Vector3(equilateralTriangleAltitude, triangleSide / 2f); //top right point
		vertices[6] = new Vector3(equilateralTriangleAltitude, -(triangleSide / 2f)); //bottom right point

		triangles = new int[] { 0, 1, 2, 0, 2, 3, 0, 3, 4, 0, 4, 5, 0, 5, 6, 0, 6, 1 };
	}
}
