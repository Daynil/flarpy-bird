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
		lastPlayedTime = 0;
		highScores = new List<HighScore>();

		for (int i = 0; i < 10; i++)
		{
			highScores.Add(new()
			{
				playerName = "default",
				score = 0
			});
		}
	}
}