using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
	[SerializeField]
	private GameObject pipe;
	[SerializeField]
	private SpriteRenderer pipeRenderer;

	[SerializeField]
	private float spawnRateSeconds = 2;
	[SerializeField]
	private int increaseSpawnRateInterval = 15;
	private float timer = 0;

	private LogicScript logic;
	private bool initialized = false;

	// Start is called before the first frame update
	public void Start()
	{
		logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
		pipeRenderer = pipe.GetComponentInChildren<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!logic.gameStarted) return;

		if (!initialized)
		{
			SpawnPipe();
			initialized = true;
		}

		if (timer < spawnRateSeconds)
		{
			// Conceptually, this adds the time in seconds since last frame
			// to the current timer, each frame. So, it's the same as a regular
			// timer.
			timer += Time.deltaTime;
		}
		else
		{
			SpawnPipe();
			timer = 0;

			if (logic.playerScore % increaseSpawnRateInterval == 0 && spawnRateSeconds > 1)
			{
				spawnRateSeconds -= 0.1f;
			}
		}

	}

	void SpawnPipe()
	{
		int verticalPadding = 250;
		float spawnHeight = Random.Range(0 + verticalPadding, Screen.height - verticalPadding);
		Vector3 startPosition = Camera.main.ScreenToWorldPoint(
			new Vector3(
				Screen.width,
				spawnHeight,
				Camera.main.WorldToScreenPoint(transform.position).z
			)
		);
		Instantiate(
			pipe,
			startPosition + Vector3.right * pipeRenderer.bounds.size.x,
			transform.rotation
		);
	}
}