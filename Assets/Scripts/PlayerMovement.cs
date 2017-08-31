using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float maxSpeed;
    private Rigidbody2D rb;
    private Animator animator;
    public GameObject body;
    public Camera cam;

    private Vector3 playerScreenPosition;
    private Vector3 touchPosition;
    private Vector3 teleportNewPosition;
    private int direction = 1;
    private int offset = 20;
    private float teleportDistance = 10;
    private bool walking = true;
    private bool teleporting = false;
    private Touch[] touches;

    public static bool moveTrigger;
    public static bool teleportLeftTrigger;
    public static bool teleportRightTrigger;
    public static bool stopTrigger;
    public static bool tapTrigger;



    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = body.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.touchCount > 0) {
            playerScreenPosition = cam.WorldToScreenPoint(transform.position);

            touchPosition = Input.GetTouch(0).position;

            if (touchPosition.x > playerScreenPosition.x + offset && direction == -1) {
                direction = 1; 
            }
            else if (touchPosition.x < playerScreenPosition.x - offset && direction == 1) {
                direction = -1;
            }      
        }

        if (tapTrigger && transform.localScale.x == direction) {
            flip();
            tapTrigger = false;
        }

        if (!walking && animator.GetBool("walk") == true) {
            float animationTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if ( animationTime % 1 < 0.6f || 0.8f < animationTime % 1) {
                animator.SetBool("walk", false);
            }
        }

        if (teleporting == true) {
            transform.position = Vector3.Lerp(transform.position, teleportNewPosition, 1f);
            if (transform.position.x - teleportNewPosition.x < 1 && transform.position.x - teleportNewPosition.x > -1)
                teleporting = false;
        }
        else {
            if (moveTrigger)
                playerMove();
            if (stopTrigger)
                playerStop();
            if (teleportLeftTrigger)
                playerTeleportLeft();
            else if (teleportRightTrigger)
                playerTeleportRight();
        }
    }

    private void playerMove() {
        rb.velocity = new Vector2(direction * maxSpeed, rb.velocity.y);
        animator.SetBool("walk", true);
        walking = true;
        if (transform.localScale.x == direction)
            flip();
    }

    private void playerStop() {
        rb.velocity = new Vector2(rb.velocity.x/2, rb.velocity.y);
        walking = false;
        stopTrigger = false;
        moveTrigger = false;
    }
    
    private void playerTeleportLeft() {
        teleportNewPosition = new Vector3(transform.position.x - teleportDistance, transform.position.y, transform.position.z);
        teleportLeftTrigger = false;
        teleporting = true;
        if (transform.localScale.x < 0)
            flip();

    }

    private void playerTeleportRight() {
        teleportNewPosition = new Vector3(transform.position.x + teleportDistance, transform.position.y, transform.position.z);
        teleportRightTrigger = false;
        teleporting = true;
        if (transform.localScale.x > 0)
            flip();

    }

    private void flip() {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
