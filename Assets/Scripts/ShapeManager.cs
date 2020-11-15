using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShapeManager : MonoBehaviour
{
	public ShapeType defaultShapeType;
	public Button startGameButton;
	public Text scoreText;
	public Text TimerText;
	private GameObject currentShape;
	private AudioSource audioSource;
	private AudioSource startGameButtonAudioSource;
	private bool isGameStarted;
	private int score;
	private string scorePrefix = "Score: ";
	private string timerPrefix = "Time: ";

	private void OnEnable()
	{
		MenuShape.OnMenuShapeCliked += CreateNewShape;
		Shape.OnShapeDoubleCliked += OnShapeDoubleCliked;
		Shape.OnShapeSwiped += OnShapeSwiped;
	}

	private void OnDisable()
	{
		MenuShape.OnMenuShapeCliked -= CreateNewShape;
		Shape.OnShapeDoubleCliked -= OnShapeDoubleCliked;
		Shape.OnShapeSwiped -= OnShapeSwiped;
	}

	private void Start()
	{
		startGameButton.onClick.AddListener(StartGame);
		startGameButton.onClick.AddListener(PlayButtonSound);
		audioSource = GetComponent<AudioSource>();
		startGameButtonAudioSource = startGameButton.GetComponent<AudioSource>();
		CreateNewShape(defaultShapeType);
	}

	private void CreateNewShape(ShapeType shapeType)
	{
		if (currentShape && currentShape.name == shapeType.ToString())
			return;

		Destroy(currentShape);
		currentShape = new GameObject(shapeType.ToString());
		currentShape.AddComponent(Type.GetType(shapeType.ToString()));
	}

	private void OnShapeDoubleCliked()
	{
		PlayShapeSound();

		if (isGameStarted)
			SetScore(1);
	}

	private void OnShapeSwiped()
	{
		PlayShapeSound();

		if (isGameStarted)
			SetScore(3);
	}

	private void StartGame()
	{
		isGameStarted = true;
		score = 0;
		scoreText.text = scorePrefix + "0";
		StopAllCoroutines();
		StartCoroutine(RunTimer());
	}

	private void SetScore(int amount)
	{
		score += amount;
		scoreText.text = scorePrefix + score.ToString();
	}

	private IEnumerator RunTimer()
	{
		int timer = 30;
		TimerText.text = timerPrefix + timer.ToString();

		while (timer > 0)
		{
			yield return new WaitForSeconds(1f);
			timer--;
			TimerText.text = timerPrefix + timer.ToString();
		}

		isGameStarted = false;
	}

	private void PlayShapeSound()
	{
		audioSource.Play();
	}

	private void PlayButtonSound()
	{
		startGameButtonAudioSource.Play();
	}
}
