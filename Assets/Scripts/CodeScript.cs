using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeScript : MonoBehaviour {

	public Sprite[] eutemiaLetters;
	public int codeId;

	public bool checkSolved(){


		for(int i = 0; i < transform.childCount; i++){
			if (transform.GetChild(i).GetComponent<LetterScript>().letter != 0) {
				return false;
			}
		}

		return true;
	}

	// Use this for initialization
	void Start () {

	}
		
	// Update is called once per frame
	void Update () {
		
	}
}
