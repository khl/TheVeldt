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
    private int direction = 1;
    private int offset = 20;

    private float[] startTime = new float[2];
    private float[] touchTime = new float[2];

    private float tapMaxTime = 0.2f;

    private Touch[] touches;
    //


    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = body.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        if (Input.touchCount > 0) {

            //Direction Handling
            playerScreenPosition = cam.WorldToScreenPoint(transform.position);
            touchPosition = Input.GetTouch(0).position;
            if (touchPosition.x > playerScreenPosition.x + offset) {
                direction = 1;
            }
            else if (touchPosition.x < playerScreenPosition.x - offset) {
                direction = -1;
            }
            //

            touches = Input.touches;

            //Touch Differentiation
            for (int i = 0; i < Input.touchCount; i++) {


                if (touches[i].phase == TouchPhase.Began) {
                    startTime[i] = Time.time;
                }

                else if (touches[i].phase == TouchPhase.Ended || touches[i].phase == TouchPhase.Canceled) {
                    if (touchTime[i] < tapMaxTime) {
                        Vector3 mousePos = cam.ScreenToWorldPoint(touches[i].position);
                        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                        //if (hit.collider != null) {
                        //}
                    }
                }
                touchTime[i] = Time.time - startTime[i];

                if (touchTime[i] > tapMaxTime) {
                    rb.velocity = new Vector3 (maxSpeed*direction,0);                   
                }
            }
        }

        if (rb.velocity.x > 5 || rb.velocity.x < -5) {
            animator.SetBool("walk", true);
        }
        else {
            animator.SetBool("walk", false);
        }

        if (transform.localScale.x * direction < 0) {
            flip();
        }    
    }

    private void flip() {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
