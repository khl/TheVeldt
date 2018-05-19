using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternScript : MonoBehaviour {

	public int codeId = -1;
	public Color solvedColor;
	private SpriteRenderer spriteRenderer; 

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();

		if (codeId != -1) {
			if (GameManager.gameData.codes [codeId] == true) {
				spriteRenderer.color = solvedColor;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
