using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

// Add "com.unity.nuget.newtonsoft-json": "3.0.2" to Packages/manifest.json dependencies
using Newtonsoft.Json;

public class FileDataHandler
{
	private string saveBasePath;

	public FileDataHandler(string saveBasePath)
	{
		this.saveBasePath = saveBasePath;
	}

	public GameData Load(string profileName)
	{
		string savePath = Path.Combine(
			saveBasePath, profileName, "data"
		);

		if (File.Exists(savePath))
		{
			string serializedData = File.ReadAllText(savePath);
			GameData deserialized = JsonConvert.DeserializeObject<GameData>(serializedData, new JsonSerializerSettings
			{
				// If a serialized List had items, by default constructor 
				// initialization list additions are appended instead of replaced
				ObjectCreationHandling = ObjectCreationHandling.Replace
			});
			return deserialized;
		}
		else
		{
			return new GameData();
		}
	}

	public void Save(GameData data, string profileName)
	{
		string savePath = Path.Combine(
			saveBasePath, profileName, "data"
		);

		Directory.CreateDirectory(Path.GetDirectoryName(savePath));

		string serializedData = JsonConvert.SerializeObject(data);
		File.WriteAllText(savePath, serializedData);
	}
}