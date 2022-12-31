using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{

	[SerializeField]
	private float moveSpeed = 0.75f;

	private int removalBufferPixels = 150;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		transform.position = transform.position + Vector3.left * moveSpeed * Time.deltaTime;

		if (transform.position.x < Camera.main.ScreenToWorldPoint(new Vector3(-removalBufferPixels, 0, 0)).x)
		{
			Destroy(gameObject);
		}
	}
}
