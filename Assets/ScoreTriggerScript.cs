using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTriggerScript : MonoBehaviour
{

	[SerializeField]
	private LogicScript logic;

	private AudioSource audioSource;
	[SerializeField]
	private AudioClip pointPing;

	public static event Action<int> OnScorePoint;

	// Start is called before the first frame update
	void Start()
	{
		audioSource = gameObject.GetComponent<AudioSource>();
		logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (logic.gameOver) return;

		if (collision.gameObject.layer == 3)
		{
			audioSource.PlayOneShot(pointPing);
			OnScorePoint?.Invoke(1);
			// logic.AddScore(1);
		}
	}
}
