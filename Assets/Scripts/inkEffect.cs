using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inkEffect : MonoBehaviour {

	public Sprite[] inkSprites;
	private Animator inkEffectAnimator;
	private float animationLength;

	// Use this for initialization
	void Start () {
		inkEffectAnimator = GetComponent<Animator> ();
		animationLength = inkEffectAnimator.GetCurrentAnimatorStateInfo (0).length;
		GetComponent<SpriteRenderer> ().sprite = inkSprites[Random.Range(0,inkSprites.Length)];
		Destroy (gameObject, animationLength-0.1f);
	}
	
	// Update is called once per frame
	void Update () { 

	}
}
