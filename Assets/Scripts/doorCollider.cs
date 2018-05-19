using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorCollider : MonoBehaviour {

	bool collided = false;

	private GameObject character;
	private Scene currentScene;

	public string sceneToLoad;
	public Vector2 newCharacterPosition;

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.GetComponent<Rigidbody2D> ().velocity.x * transform.lossyScale.x > 0) {
			if (!collided) {
				currentScene = SceneManager.GetActiveScene ();
				character = coll.gameObject;
				SceneManager.LoadScene (sceneToLoad, LoadSceneMode.Additive);
				SceneManager.sceneLoaded += SceneLoaded;
				collided = true;
			}
		}
	}


	public void SceneLoaded(Scene newScene, LoadSceneMode loadMode){
		SceneManager.sceneLoaded -= SceneLoaded;
		character.transform.position = newCharacterPosition;
		SceneManager.MoveGameObjectToScene(character, SceneManager.GetSceneByName(sceneToLoad));
		SceneManager.UnloadSceneAsync (currentScene);
		SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;

		GameManager.gameData.currentScene = sceneToLoad;

		collided = false;
	}

	void SceneManager_sceneUnloaded (Scene arg0){
		character.GetComponent<TouchHandling>().getCamera();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
