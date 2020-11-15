using UnityEngine;
using UnityEngine.UI;

public class MenuShape : MonoBehaviour
{
	public ShapeType shapeType;
	private Button button;
	private AudioSource audioSource;

	public delegate void MenuShapeClikedEvent(ShapeType shapeType);
	public static event MenuShapeClikedEvent OnMenuShapeCliked;

	private void Start()
	{
		button = GetComponent<Button>();
		audioSource = GetComponent<AudioSource>();
		button.onClick.AddListener(ChangeShape);
		button.onClick.AddListener(PlaySound);
	}

	private void ChangeShape()
	{
		OnMenuShapeCliked?.Invoke(shapeType);
	}

	private void PlaySound()
	{
		audioSource.Play();
	}
}
