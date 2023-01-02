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

	// Start is called before the first frame update
	void Start()
	{
		audioSource = gameObject.GetComponent<AudioSource>();
		logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 3)
		{
			audioSource.PlayOneShot(pointPing);
			logic.AddScore(1);
		}
	}
}
