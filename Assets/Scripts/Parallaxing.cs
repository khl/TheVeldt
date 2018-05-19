using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing = 1f;

    public Transform cam;
    private Vector3 previousCamPos;

    void Awake() {
    
	}

	// Use this for initialization
	void Start () {
		previousCamPos = GameObject.Find ("Player").transform.position;

        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++) {
            parallaxScales[i] = backgrounds[i].position.z;
        }
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i <backgrounds.Length; i++) {
            float parallax = (cam.position.x - previousCamPos.x) * parallaxScales[i];

			Vector3 backgroundTargetPos = new Vector3 (backgrounds[i].position.x + parallax, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing);
        }

        previousCamPos = cam.position;
	}
}
