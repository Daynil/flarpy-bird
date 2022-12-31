using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
	[SerializeField]
	private Rigidbody2D myRigidbody;
	[SerializeField]
	private float flapStrength = 1.75f;
	[SerializeField]
	private bool birdIsAlive = true;

	[SerializeField]
	private LogicScript logic;

	// Start is called before the first frame update
	void Start()
	{
		logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
		{
			myRigidbody.velocity = Vector2.up * flapStrength;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		birdIsAlive = false;
		logic.GameOver();
	}
}
