using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().color = GameManager.lightColors[GameManager.gameData.lightColor];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
