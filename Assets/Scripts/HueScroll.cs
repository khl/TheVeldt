using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HueScroll : MonoBehaviour {

    private SpriteRenderer mySpriteRenderer;

    // Use this for initialization
    void Start() {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        mySpriteRenderer.color = new Color(1, 0, 0, 0.2f);
    }

    // Update is called once per frame
    void Update() {

        if (mySpriteRenderer.color.r >= 1) {
            if (mySpriteRenderer.color.b > 0)
                mySpriteRenderer.color = new Color(1, mySpriteRenderer.color.g, mySpriteRenderer.color.b - 0.01f, 0.2f);
            else
                mySpriteRenderer.color = new Color(1, mySpriteRenderer.color.g + 0.01f, 0, 0.2f);
        }
        if (mySpriteRenderer.color.g >= 1) {
            if (mySpriteRenderer.color.r > 0) 
                mySpriteRenderer.color = new Color(mySpriteRenderer.color.r - 0.01f, mySpriteRenderer.color.g, mySpriteRenderer.color.b, 0.2f);          
            else
                mySpriteRenderer.color = new Color(mySpriteRenderer.color.r, mySpriteRenderer.color.g, mySpriteRenderer.color.b + 0.01f, 0.2f);
        }
        if (mySpriteRenderer.color.b >= 1) {
            if (mySpriteRenderer.color.g > 0)
                mySpriteRenderer.color = new Color(mySpriteRenderer.color.r, mySpriteRenderer.color.g - 0.01f, mySpriteRenderer.color.b, 0.2f);
            else
                mySpriteRenderer.color = new Color(mySpriteRenderer.color.r+0.01f, mySpriteRenderer.color.g, mySpriteRenderer.color.b, 0.2f);
        }
    }
}
