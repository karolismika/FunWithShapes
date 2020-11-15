using UnityEngine;
using UnityEngine.EventSystems;

public enum ShapeType
{
	Hexagon,
	Square,
	Triangle,
	Trapezoid,
	Octagon
}

public class Shape : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public Vector3[] vertices;
	public Vector2[] uv;
	public int[] triangles;
	private Vector2 downPosition;
	private MeshFilter meshFilter;
	private MeshRenderer meshRenderer;
	private MeshCollider meshCollider;
	private Mesh mesh;
	private Material material;
	private Color currentColor;
	private float lastTapTime;
	private bool isInitiallyClicked;
	private readonly float doubleTapSpeed = 0.5f;
	private readonly float rotationSpeed = 20f;
	private readonly float pingPongSpeed = 0.2f;
	private readonly Vector3 pingPongLeft = new Vector3(-4f, 0f, 0f);
	private readonly Vector3 pingPongRight = new Vector3(4f, 0f, 0f);

	public delegate void ShapeDoubleClikedEvent();
	public static event ShapeDoubleClikedEvent OnShapeDoubleCliked;

	public delegate void ShapeSwipedEvent();
	public static event ShapeSwipedEvent OnShapeSwiped;

	private void Start()
	{
		CreateShape();
	}

	private void Update()
	{
		Rotate();
		PingPong();
	}

	public virtual void CalculateShape()
	{

	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (IsDoubleTap)
		{
			SetNewRandomColor();
			OnShapeDoubleCliked?.Invoke();
		}

		downPosition = eventData.pointerCurrentRaycast.worldPosition;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (IsSwipe(eventData.pointerCurrentRaycast.worldPosition))
		{
			SetNewRandomColor();
			OnShapeSwiped?.Invoke();
		}
	}

	private void CreateShape()
	{
		meshFilter = gameObject.AddComponent<MeshFilter>();
		meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshCollider = gameObject.AddComponent<MeshCollider>();
		mesh = new Mesh();

		CalculateShape();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		meshFilter.mesh = mesh;
		meshCollider.sharedMesh = mesh;
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		mesh.RecalculateTangents();
		SetUV();
		CreateMaterial(meshRenderer);
	}

	public void SetUV()
	{
		uv = new Vector2[vertices.Length];
		float meshWidth = mesh.bounds.extents.x * 2f;
		float meshHeight = mesh.bounds.extents.y * 2f;
		float divisor = meshWidth > meshHeight ? meshHeight : meshWidth;

		for (int i = 0; i < vertices.Length; i++)
			uv[i] = new Vector2(vertices[i].x / divisor + 0.5f, vertices[i].y / divisor + 0.5f);

		mesh.uv = uv;
	}

	private void Rotate()
	{
		transform.localEulerAngles -= new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, Time.deltaTime * rotationSpeed);
	}

	private void PingPong()
	{
		float t = Mathf.PingPong(Time.time * pingPongSpeed, 1f);
		transform.localPosition = Vector3.Lerp(pingPongLeft, pingPongRight, t);
	}

	private bool IsDoubleTap
	{
		get
		{
			bool val = false;

			if (!isInitiallyClicked)
				isInitiallyClicked = true;
			else if (Time.time - lastTapTime <= doubleTapSpeed)
			{
				val = true;
				isInitiallyClicked = false;
			}

			lastTapTime = Time.time;

			return val;
		}
	}

	private bool IsSwipe(Vector2 upPosition)
	{
		bool val = Vector2.Distance(downPosition, upPosition) > 1.2f && Time.time - lastTapTime < 0.5f;

		return val;
	}

	private void SetNewRandomColor()
	{
		material.color = NewRandomColor;
	}

	private Color NewRandomColor
	{
		get
		{
			Color color = RandomColor;

			while (color == currentColor)
				color = RandomColor;

			currentColor = color;

			return color;
		}
	}

	private Color RandomColor
	{
		get
		{
			Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

			return color;
		}
	}

	public void CreateMaterial(MeshRenderer meshRenderer)
	{
		material = new Material(Shader.Find("Sprites/Default"))
		{
			color = NewRandomColor,
			mainTexture = (Texture)Resources.Load("Monster")
		};


		meshRenderer.material = material;
	}
}
