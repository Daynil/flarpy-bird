using System.Collections;
using System.Collections.Generic;

public class HighScore
{
	public string playerName;
	public int score;
}

public class GameData
{
	public long lastPlayedTime;
	public List<HighScore> highScores;

	public GameData()
	{
		string startNameLetters = "ABCDEFGHIJ";
		lastPlayedTime = 0;
		highScores = new List<HighScore>();

		for (int i = 0; i < startNameLetters.Length; i++)
		{
			highScores.Add(new()
			{
				playerName = new string(startNameLetters[i], 3),
				score = 0
			});
		}
	}
}