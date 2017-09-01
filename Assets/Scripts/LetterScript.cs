using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterScript : MonoBehaviour {

	[SerializeField] private Sprite[] letters = new Sprite[26];
    [SerializeField] private int count;
    private SpriteRenderer mySpriteRenderer;

    // Use this for initialization
    void Start () {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        mySpriteRenderer.sprite = letters[count];
    }

    public void clicked() {
        count++;
        if (count >= 26)
            count = 0;
        mySpriteRenderer.sprite = letters[count];
    }


    // Update is called once per frame
    void Update () {
       
	}
}
