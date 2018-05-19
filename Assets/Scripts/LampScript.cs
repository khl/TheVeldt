using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampScript : MonoBehaviour {

	SpriteRenderer spriteRenderer;

	public enum LampType {introLamp, glowLamp, lightsLamp};
	public LampType lampType;

	public SpriteRenderer[] frontGlows;
	public SpriteRenderer[] backGlows;
	public int backGlowAlpha;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();

		if (lampType == LampType.introLamp) {
			spriteRenderer.color = GameManager.lampColors [GameManager.gameData.lampColor];
		} else if (lampType == LampType.glowLamp) {
			spriteRenderer.color = GameManager.glowColors [GameManager.gameData.glowColor];
		} else {
			spriteRenderer.color = GameManager.lightColors [GameManager.gameData.lightColor];
		}
	}

	public void clicked(){
		//scroll through colors

		if (lampType == LampType.introLamp) {
			GameManager.gameData.lampColor++;
			if (GameManager.gameData.lampColor >= GameManager.lampColors.Length) {
				GameManager.gameData.lampColor = 0;
			}

			if (GameManager.gameData.lampColor == 1) {
				GameManager.gameData.codes [3] = true;
			} else {
				GameManager.gameData.codes [3] = false;
			}

			spriteRenderer.color = GameManager.lampColors [GameManager.gameData.lampColor];



			for (int i = 0; i < frontGlows.Length; i++) {
				frontGlows [i].color = GameManager.lampColors [GameManager.gameData.lampColor];
				backGlows [i].color = new Color (
					GameManager.lampColors [GameManager.gameData.lampColor].r,
					GameManager.lampColors [GameManager.gameData.lampColor].g,
					GameManager.lampColors [GameManager.gameData.lampColor].b,
					backGlowAlpha
				);
			}
		} else if (lampType == LampType.glowLamp) {
			GameManager.gameData.glowColor++;

			if (GameManager.gameData.glowColor >= GameManager.glowColors.Length) {
				GameManager.gameData.glowColor = 0;
			}

			spriteRenderer.color = GameManager.glowColors [GameManager.gameData.glowColor];

			for (int i = 0; i < frontGlows.Length; i++) {
				frontGlows [i].color = GameManager.glowColors [GameManager.gameData.glowColor];
				backGlows [i].color = new Color (
					GameManager.glowColors [GameManager.gameData.glowColor].r,
					GameManager.glowColors [GameManager.gameData.glowColor].g,
					GameManager.glowColors [GameManager.gameData.glowColor].b,
					backGlowAlpha
				);
			}
		} else {
			GameManager.gameData.lightColor++;

			if (GameManager.gameData.lightColor >= GameManager.lightColors.Length) {
				GameManager.gameData.lightColor = 0;
			}

			if (GameManager.gameData.lightColor == 1) {
				GameManager.gameData.codes [7] = true;
				GameManager.gameData.codes [6] = false;
			} else {
				GameManager.gameData.codes [6] = true;
				GameManager.gameData.codes [7] = false;

			}

			spriteRenderer.color = GameManager.lightColors [GameManager.gameData.lightColor];

			for (int i = 0; i < frontGlows.Length; i++) {
				frontGlows [i].color = GameManager.lightColors [GameManager.gameData.lightColor];
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
