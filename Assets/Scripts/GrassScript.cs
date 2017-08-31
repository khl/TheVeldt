using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassScript : MonoBehaviour {

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.Play("grassAnimationForward",-1, Random.Range(0.0f, 0.95f));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
