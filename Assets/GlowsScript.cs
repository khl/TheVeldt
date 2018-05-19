using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().color = GameManager.glowColors[GameManager.gameData.glowColor];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
