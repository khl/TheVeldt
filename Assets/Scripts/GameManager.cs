using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameData gameData = new GameData ();
	public static Color[] lampColors = new Color[] {new Color(1, 1, 1), new Color(1, 0f, 0f)};
	public static Color[] glowColors = new Color[] {new Color(0.8f, 0, 0.8f), new Color(0, 0.4f, 0.8f), new Color(0, 0.6f, 0.8f), new Color(0, 0.8f, 0)};
	public static Color[] lightColors = new Color[] {new Color(1, 1, 1), new Color(0, 0, 0)};

	public Transform canvas;
	private GameObject player;

	public GameObject[] newButtonPrefabs = new GameObject[2];
	public GameObject[] saveButtonPrefabs = new GameObject[2];

	void Start () {
		DontDestroyOnLoad (gameObject);
	
		File.Delete (Application.persistentDataPath + "/Save"+ 0 +".dat");
		File.Delete (Application.persistentDataPath + "/Save"+ 1 +".dat");

		//Instantiate Buttons
		for (int i = 0; i < 2; i++) {
			if (File.Exists(Application.persistentDataPath + "/Save"+ i +".dat")) {
				Instantiate (saveButtonPrefabs [i]).transform.SetParent(canvas);
			} else {
				Instantiate (newButtonPrefabs [i]).transform.SetParent(canvas);
			}
		}
		//
	}
		
	void OnApplicationQuit(){
		save (gameData.saveNumber);
	}

	void OnApplicationPause(bool paused){
		if (paused) {
			save (gameData.saveNumber);
		}
	}
		
	void OnDisable(){
		save (gameData.saveNumber);
	}

	void OnDestroy(){
		save (gameData.saveNumber);
	}

	public void newGame(int saveNumber){
		gameData = new GameData ();
		gameData.saveNumber = saveNumber;
		save (saveNumber);
		loadGame ();
	}

	public void save(int saveNumber){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/Save"+saveNumber+".dat", FileMode.OpenOrCreate);
		bf.Serialize (file, gameData);
		file.Close ();
	}

	public void load(int saveNumber){
		if(File.Exists(Application.persistentDataPath + "/Save"+saveNumber+".dat")){
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(Application.persistentDataPath + "/Save"+saveNumber+".dat", FileMode.Open);
			gameData = (GameData)bf.Deserialize(file);
			file.Close ();
			gameData.saveNumber = saveNumber;
		}
		loadGame ();
	}
		
	//Load Game Procedures//
	private Scene currentScene;

	private void loadGame(){
		currentScene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (gameData.currentScene, LoadSceneMode.Additive);
		SceneManager.sceneLoaded += SceneLoaded;
	}


	public void SceneLoaded(Scene newScene, LoadSceneMode loadMode){
		SceneManager.sceneLoaded -= SceneLoaded;
		player = GameObject.Find ("Player");
		player.transform.position = new Vector2 (gameData.playerPosition_x, gameData.playerPosition_y);
		SceneManager.MoveGameObjectToScene(player,SceneManager.GetSceneByName(gameData.currentScene));
		SceneManager.UnloadSceneAsync (currentScene);
		SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
	}

	void SceneManager_sceneUnloaded (Scene arg0){
		player.GetComponent<TouchHandling>().getCamera();
	}
	//

}

[Serializable]
public class GameData{

	public int saveNumber;

	public bool[] codes = new bool[8];

	public String currentScene;

	public bool introTutorial;
	public int lampColor;
	public int glowColor;
	public int lightColor;

	public float playerPosition_x;
	public float playerPosition_y;

	public GameData(){
		for (int i = 0; i < codes.Length; i++) {
			codes [i] = false;
		}

		introTutorial = true;

		currentScene = "Intro";
		playerPosition_x = -4.33f;
		playerPosition_y = -2.57f;

		lampColor = 0;
		glowColor = 0;
		lightColor = 0;
	}
}