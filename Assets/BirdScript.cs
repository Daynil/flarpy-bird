using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
	[SerializeField]
	private float flapStrength = 1.15f;
	[SerializeField]
	private bool birdIsAlive = true;

	[SerializeField]
	private LogicScript logic;

	private Rigidbody2D rigidBody;
	private Animator animator;
	private ParticleSystem particle;

	private AudioSource audioSource;
	[SerializeField]
	private AudioClip flap;
	[SerializeField]
	private AudioClip splat;

	// Start is called before the first frame update
	void Start()
	{
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
		animator = gameObject.GetComponent<Animator>();
		particle = gameObject.GetComponentInChildren<ParticleSystem>();
		audioSource = gameObject.GetComponent<AudioSource>();

		logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!logic.gameStarted) return;

		if (!rigidBody.simulated)
		{
			rigidBody.simulated = true;
			animator.SetBool("gameStarted", true);

		}

		if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
		{
			rigidBody.velocity = Vector2.up * flapStrength;
			animator.Play("flap_once");
			particle.Play();
			audioSource.PlayOneShot(flap);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		birdIsAlive = false;
		audioSource.PlayOneShot(splat);
		logic.GameOver();
	}
}
