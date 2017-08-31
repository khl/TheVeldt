using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TouchStatus { none, tap, hold, leftSwipe, rightSwipe };

public class TouchManager : MonoBehaviour {

    public static TouchStatus[] touchStatus = new TouchStatus[2];
    private Vector3 touchPosition;
    private Vector3[] startPosition = new Vector3[2];

    private float[] startTime = new float[2];
    private float touchDistance;
    private float[] touchTime = new float[2];

    private float tapMaxTime = 0.2f;
    private float swipeMaxTime = 0.3f;
    private float swipeMinDistance = 15000;
    private float holdMinTime = 0.1f;

    private Touch[] touches;

    private void Start() {

    }
    // Update is called once per frame
    private void Update() {

        if (Input.touchCount > 0) {
            touches = Input.touches;
            for (int i = 0; i < Input.touchCount; i++) {

                touchTime[i] = Time.time - startTime[i];

                if (touches[i].phase == TouchPhase.Began) {
                    startTime[i] = Time.time;
                    startPosition[i] = touches[i].position;
                }
                else if (touches[i].phase == TouchPhase.Ended || touches[i].phase == TouchPhase.Canceled) {
                    touchPosition = touches[i].position;
                    Vector2 distance = touchPosition - startPosition[i];
                    touchDistance = (distance).sqrMagnitude;

                    if (PlayerMovement.moveTrigger) {
                        PlayerMovement.stopTrigger = true;
                    }

                    if (touchTime[i] < swipeMaxTime && touchDistance > swipeMinDistance) {

                        if (distance.x > 0)
                            PlayerMovement.teleportRightTrigger = true;
                        else
                            PlayerMovement.teleportLeftTrigger = true;

                    }            
                    else if (touchTime[i] < tapMaxTime) {
                        PlayerMovement.tapTrigger = true;
                    }

                }
                else if (touches[i].phase == TouchPhase.Moved || touches[i].phase == TouchPhase.Stationary) {
                    if (touchTime[i] > holdMinTime)
                        PlayerMovement.moveTrigger = true;
                }
            }
        }
    }
}
