using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LogicScript : MonoBehaviour, IDataPersist
{
	[SerializeField]
	public int playerScore = 0;
	[SerializeField]
	private TMP_Text scoreText;

	[SerializeField]
	private GameObject gameOverScreen;
	[SerializeField]
	private GameObject startScreen;

	public bool gameStarted = false;
	public bool gameOver = false;

	[SerializeField]
	private GameObject highScoreScreen;
	[SerializeField]
	private TMP_Text highScore;

	[SerializeField] private GameObject newHighScoreScreen;
	[SerializeField] private TMP_Text scorePlace;
	[SerializeField] private TMP_InputField scoreName;
	[SerializeField] private TMP_Text nameError;

	private List<HighScore> highScores;

	private void OnEnable()
	{
		ScoreTriggerScript.OnScorePoint += AddScore;
	}

	private void OnDisable()
	{
		ScoreTriggerScript.OnScorePoint -= AddScore;
	}

	[ContextMenu("Increase Score")]
	public void AddScore(int scoreToAdd)
	{
		playerScore += scoreToAdd;
		scoreText.text = playerScore.ToString();
	}

	public void StartGame()
	{
		startScreen.SetActive(false);
		gameStarted = true;
	}

	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void GameOver()
	{
		if (gameOver) return;

		gameOver = true;

		if (playerScore > highScores[^1].score)
		{
			newHighScoreScreen.SetActive(true);
			int highScorePlace = highScores.FindIndex(
				score => score.score < playerScore
			);
			string suffix = "th";
			switch (highScorePlace + 1)
			{
				case 1:
					suffix = "st";
					break;
				case 2:
					suffix = "nd";
					break;
				case 3:
					suffix = "rd";
					break;
			}
			scorePlace.text = $"{highScorePlace + 1}{suffix}";
		}
		else
		{
			gameOverScreen.SetActive(true);
		}

	}

	public void DoneHighScoreName()
	{
		if (scoreName.text.Length == 0)
		{
			nameError.gameObject.SetActive(true);
		}
		else
		{
			int highScorePlace = highScores.FindIndex(
				score => score.score < playerScore
			);

			highScores.Insert(
				highScorePlace,
				new() { playerName = scoreName.text, score = playerScore }
			);
			highScores.RemoveAt(highScores.Count - 1);

			DataPersistenceManager.instance.SaveGame();
			ShowHighScores();
		}
	}

	public void NameEntry()
	{
		if (nameError.gameObject.activeSelf)
		{
			if (scoreName.text.Length > 0)
			{
				nameError.gameObject.SetActive(false);
			}
		}
	}

	public void ShowHighScores()
	{
		newHighScoreScreen.SetActive(false);
		gameOverScreen.SetActive(false);
		highScoreScreen.SetActive(true);

		string highScoreText = "";

		for (int i = 0; i < highScores.Count; i++)
		{
			if (i > 0) highScoreText += "<br>";
			highScoreText += $"{i + 1}: {highScores[i].playerName}  {highScores[i].score}";
		}

		highScore.text = highScoreText;
	}

	public void LoadData(GameData data)
	{
		highScores = data.highScores;
	}

	public void SaveData(GameData data)
	{
		data.highScores = highScores;
	}
}
