using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

// https://www.youtube.com/watch?v=mntS45g8OK4
// https://github.com/llamacademy/persistent-data/blob/main/Assets/Scripts/JSONDataService.cs
class SaveData
{
	public List<int> HighScores;
}

public class LogicScript : MonoBehaviour
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
		LoadSavedgame();
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
		SaveGame(playerScore);
	}

	public void SaveGame(int newScore)
	{
		SaveData newSave = new SaveData
		{
			HighScores = new List<int>
			{
				newScore
			}
		};

		string saveJson = JsonConvert.SerializeObject(newSave);

		File.WriteAllText(Application.persistentDataPath + "/saveData.json", saveJson);
		Debug.Log("written to: " + Application.persistentDataPath);
	}

	public void LoadSavedgame()
	{
		if (File.Exists(Application.persistentDataPath + "/saveData.json"))
		{
			string existingData = File.ReadAllText(Application.persistentDataPath + "/saveData.json");
			Debug.Log(existingData);
			saveData = JsonConvert.DeserializeObject<SaveData>(existingData);
			Debug.Log("Loaded save data: " + saveData);
		}
		else
		{
			saveData = new SaveData
			{
				HighScores = new List<int> {
					0
				}
			};
			Debug.Log("no save data yet");
		}
	}
}
