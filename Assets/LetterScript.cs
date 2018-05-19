using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterScript : MonoBehaviour {

	public int[] letterOptions = new int[3];
	public int letter;
	public GameObject letterSplatter;
	private CodeScript codeScript;

	private SpriteRenderer spriteRenderer;

	public void hit(){
		letter++;
		if (letter == letterOptions.Length) {
			letter = 0;
		}

		spriteRenderer.sprite = codeScript.eutemiaLetters [letterOptions [letter]];
		GameManager.gameData.codes[codeScript.codeId] = codeScript.checkSolved ();
			
		//Letter Splatter Effect
		Transform temp = Instantiate (letterSplatter).transform;
		temp.position = transform.position;
		temp.Rotate(0,0,Random.Range(0,360)); 
		//

	}

	void Start () {

		codeScript = GetComponentInParent<CodeScript> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		if (GameManager.gameData.codes [codeScript.codeId]) { //if solved
			letter = 0;
		} else {
			letter = Random.Range(0,letterOptions.Length);
		}
	
		spriteRenderer.sprite = codeScript.eutemiaLetters[letterOptions [letter]];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
