using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour {

	public Animator lockedDisplay;
	public int codeId = -1;
	public GameObject playerCollider;
	Animator animator;
	public bool colorDoor = false;
	public int color;

	public void clicked(){

		if (animator.GetBool ("doorOpen")) {
			animator.SetBool ("doorOpen", false);
			playerCollider.SetActive (false);
		} else {
			if (colorDoor) {
				if (color == GameManager.gameData.glowColor) {
					GetComponent<Animator> ().SetBool ("doorOpen", true);
					playerCollider.SetActive (true);
				} else {
					if (!lockedDisplay.GetCurrentAnimatorStateInfo (0).IsName ("lockedDisplayAnim")) {
						lockedDisplay.SetTrigger ("lockedAnimTrigger");
					}
				}
			} else if (codeId == -1) {
				GetComponent<Animator> ().SetBool ("doorOpen", true);
				playerCollider.SetActive (true);
			} else {
				if (GameManager.gameData.codes[codeId]) {
					GetComponent<Animator> ().SetBool ("doorOpen", true);
					playerCollider.SetActive (true);
				} else {
					if (!lockedDisplay.GetCurrentAnimatorStateInfo (0).IsName ("lockedDisplayAnim")) {
						lockedDisplay.SetTrigger ("lockedAnimTrigger");
					}
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
