using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

// https://www.youtube.com/watch?v=mntS45g8OK4
// https://github.com/llamacademy/persistent-data/blob/main/Assets/Scripts/JSONDataService.cs

// https://www.youtube.com/watch?v=aUi9aijvpgs
// https://github.com/trevermock/save-load-system/tree/5-bug-fixes-and-polish
public class DataPersistenceManager : MonoBehaviour
{
	[SerializeField] private bool autosave = false;
	[SerializeField] private int autoSaveIntervalSeconds = 60;

	[SerializeField] private string saveProfileName = "default";

	private GameData gameData;
	private List<IDataPersist> dataPersistenceObjects;
	private FileDataHandler fileDataHandler;

	private Coroutine autoSaveCoroutine;

	public static DataPersistenceManager instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.Log("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
			Destroy(gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(gameObject);

		Debug.Log(Application.persistentDataPath);
		fileDataHandler = new FileDataHandler(Application.persistentDataPath);
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnApplicationQuit()
	{
		SaveGame();
	}

	public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		dataPersistenceObjects = new List<IDataPersist>(
			FindObjectsOfType<MonoBehaviour>(true)
				.OfType<IDataPersist>()
			);
		LoadGame();

		if (autosave)
		{
			if (autoSaveCoroutine != null)
			{
				StopCoroutine(autoSaveCoroutine);
			}
			autoSaveCoroutine = StartCoroutine(AutoSave());
		}
	}

	public void SaveGame()
	{
		foreach (IDataPersist dataPersistObject in dataPersistenceObjects)
		{
			dataPersistObject.SaveData(gameData);
		}

		gameData.lastPlayedTime = System.DateTime.Now.ToBinary();
		fileDataHandler.Save(gameData, saveProfileName);
	}

	public void LoadGame()
	{
		gameData = fileDataHandler.Load(saveProfileName);

		foreach (IDataPersist dataPersistObject in dataPersistenceObjects)
		{
			dataPersistObject.LoadData(gameData);
		}
	}

	private IEnumerator AutoSave()
	{
		while (true)
		{
			yield return new WaitForSeconds(autoSaveIntervalSeconds);
			SaveGame();
		}
	}
}