using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
	[SerializeField]
	private int playerScore = 0;
	[SerializeField]
	private Text scoreText;
	[SerializeField]
	private GameObject gameOverScreen;

	[ContextMenu("Increase Score")]
	public void AddScore(int scoreToAdd)
	{
		playerScore += scoreToAdd;
		scoreText.text = playerScore.ToString();
	}

	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void GameOver()
	{
		gameOverScreen.SetActive(true);
	}
}
