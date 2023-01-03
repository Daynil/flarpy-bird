using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

// https://www.youtube.com/watch?v=mntS45g8OK4
// https://github.com/llamacademy/persistent-data/blob/main/Assets/Scripts/JSONDataService.cs

// https://www.youtube.com/watch?v=aUi9aijvpgs
// https://github.com/trevermock/save-load-system/tree/5-bug-fixes-and-polish
class SaveData
{
	public List<int> HighScores;
}

public class LogicScript : MonoBehaviour, IDataPersist
{
	[SerializeField]
	public int playerScore = 0;
	[SerializeField]
	private Text scoreText;

	private SaveData saveData;

	[SerializeField]
	private GameObject gameOverScreen;
	[SerializeField]
	private GameObject startScreen;

	public bool gameStarted = false;

	private void Start()
	{
		// LoadSavedgame();
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
		gameOverScreen.SetActive(true);
		// SaveGame(playerScore);
	}

	public void LoadData(GameData data)
	{
		foreach (HighScore score in data.highScores)
		{
			Debug.Log(score.playerName + " : " + score.score);
		}
	}

	public void SaveData(GameData data)
	{
		if (playerScore == 0) return;

		if (playerScore > data.highScores[data.highScores.Count - 1].score)
		{
			for (int i = 0; i < data.highScores.Count; i++)
			{
				if (playerScore > data.highScores[i].score)
				{
					data.highScores.Insert(i, new() { playerName = "default", score = playerScore });
					data.highScores.RemoveAt(data.highScores.Count - 1);
					break;
				}
			}
		}
	}
}
