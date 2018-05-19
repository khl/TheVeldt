using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBackgroundScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().color = GameManager.lampColors[GameManager.gameData.lampColor];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
