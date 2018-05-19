using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchHandling : MonoBehaviour {

	public GameObject inkEffectPrefab;

	public static Camera cam;

	private Transform character;
	private Animator characterAnimator;
	private Rigidbody2D characterRigidbody;

	private int characterDirection = 1;
	[SerializeField] private float maxSpeed;
	private float minHoldTime = 0.2f;

	private Touch[] touches;
	private List<float> touchesStartTime = new List<float>(); //tracks touch start time in tenths of a second
	private List<float> previousInteractionTime = new List<float>(); //tracks the time of the previous interaction of each touch
	private List<float> interactionCooldown = new List<float>(); //tracks cooldown for next interaction for each touch
	private List<bool> move = new List<bool>(); //tracks if this touch is a move touch or an interaction touch

	public void getCamera(){
		cam = Camera.main;
	}

	void Start(){
		getCamera ();
		character = transform;
		characterRigidbody = character.GetComponent<Rigidbody2D> ();
		characterAnimator = character.GetChild(0).GetComponent<Animator> ();
	}
	void Awake () {
		
	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {

			touches = Input.touches;

			int indexForDirection = 0;

			for (int i = 0; i < touches.Length; i++) {

				Vector3 touchWorldPoint = cam.ScreenToWorldPoint (touches [i].position);
				touchWorldPoint.z = 0;

				Transform newInk = Instantiate (inkEffectPrefab).transform;
				newInk.position = touchWorldPoint;
				newInk.Rotate (0, 0, Random.Range (0, 360));

				if (touches [i].phase == TouchPhase.Began) {
					
					RaycastHit2D hit = Physics2D.Raycast (touchWorldPoint, cam.transform.forward);

					if (hit.collider != null) { //This is a touch intended for interacting, it CAN change into a moving touch, but not back
						if (move.Count <= i) {
							move.Add (false); 
						} else {
							move [i] = false; 
						}

						if (previousInteractionTime.Count <= i) {
							previousInteractionTime.Add (-Time.time - 5); 
							interactionCooldown.Add (1);
						} else {
							previousInteractionTime [i] = -Time.time - 5; 
							interactionCooldown [i] = 1;
						}

					} else { //This is a touch intended for moving the character, it CANNOT change into an interaction touch
						if (move.Count <= i) {
							move.Add (true); 
						} else {
							move [i] = true; 
						}
						//Adds/Changes start time for touch number
						if (touchesStartTime.Count <= i) {
							touchesStartTime.Add (Time.time);
						} else {
							touchesStartTime [i] = Time.time;
						}
					}
				} else {

				}

				if (move [i]) { //if this a touch intended for moving
					//Player Movement
					if (Time.time - touchesStartTime[i] > minHoldTime) {
						indexForDirection = i; //Set the index for direction as the touch that is moving the character
						characterRigidbody.velocity = new Vector3 (maxSpeed * characterDirection, characterRigidbody.velocity.y);
						GameManager.gameData.playerPosition_x = transform.position.x;
						GameManager.gameData.playerPosition_y = transform.position.y;
					}
					//

				} else { //else its a touch intended for interacting

					RaycastHit2D hit = Physics2D.Raycast (cam.ScreenToWorldPoint (touches [i].position), cam.transform.forward);

					if (hit.collider != null) { //if something was touched
						if (Time.time - previousInteractionTime[i] >= interactionCooldown[i]) {
								
							previousInteractionTime[i] = Time.time;

							if(interactionCooldown[i] > 0.15f){
								interactionCooldown[i] /= 2;
							}

							string tag = hit.collider.tag;
							if (tag == "Letter") {
								hit.transform.GetComponent<LetterScript>().hit();
							} else if (tag == "Door") {
								hit.transform.gameObject.GetComponent<DoorScript>().clicked ();
							} else if (tag == "Lamp") {
								hit.transform.gameObject.GetComponent<LampScript>().clicked ();
							} 
						}
					} else { //nothing was touched so this touch will now qualify as a touch intended for moving the character
						move [i] = true; //This is a touch intended for moving the character, it CANNOT change into an interaction touch
						//Adds/Changes start time for touch number
						if (touchesStartTime.Count <= i) {
							touchesStartTime.Add (Time.time);
						} else {
							touchesStartTime [i] = Time.time;
						}
					}
				}
			}

			//Direction Handling
			if (touches[indexForDirection].position.x > cam.WorldToScreenPoint (character.position).x + 20) {
				characterDirection = 1;
			} else if (touches [indexForDirection].position.x < cam.WorldToScreenPoint (character.position).x - 20) {
				characterDirection = -1;
			}
			//

		}

		//Player Flipping
		if (character.localScale.x * characterDirection < 0) {
			Vector3 theScale = character.localScale;
			theScale.x *= -1;
			character.localScale = theScale;
		}    	
		//

		//Player Animation
		if (characterRigidbody.velocity.x > maxSpeed-1 || characterRigidbody.velocity.x < -maxSpeed+1) {
			characterAnimator.SetBool("walk", true);

		}
		else {
			characterAnimator.SetBool("walk", false);
		}
		//
	}



}
