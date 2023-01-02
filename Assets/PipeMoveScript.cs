using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{

	[SerializeField]
	private float moveSpeed = 0.75f;

	private int removalBufferPixels = 150;

	private int moveSpeedIncreaseInterval = 10;
	private float moveSpeedIncreaseAmount = 0.05f;
	private int scoreLastUpdated = 0;

	private LogicScript logic;

	private void Start()
	{
		logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
	}

	// Update is called once per frame
	void Update()
	{
		if (
				logic.playerScore > 0 &&
				logic.playerScore % moveSpeedIncreaseInterval == 0 &&
				moveSpeed < 2f &&
				// Avoid updating more than once at given score
				scoreLastUpdated != logic.playerScore
			)
		{
			moveSpeed += moveSpeedIncreaseAmount;
			scoreLastUpdated = logic.playerScore;
		}

		transform.position = transform.position + Vector3.left * moveSpeed * Time.deltaTime;

		if (transform.position.x < Camera.main.ScreenToWorldPoint(new Vector3(-removalBufferPixels, 0, 0)).x)
		{
			Destroy(gameObject);
		}
	}
}
