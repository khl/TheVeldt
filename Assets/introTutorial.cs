using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introTutorial : MonoBehaviour {

	public GameObject moveTutorial;

	// Use this for initialization
	void Start () {
		if (GameManager.gameData.introTutorial) {
			GameManager.gameData.introTutorial = false;
			moveTutorial.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
