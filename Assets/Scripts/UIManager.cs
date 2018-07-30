using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    GameObject characterObject;
    GameObject sign;

    public float distanceAboveHead = 1f;
    public int fontSize = 50;

    public CardManager cardManager;

   void Start() {
        sign = new GameObject("player_label");
        characterObject = GameObject.Find("Player");
        cardManager = GameObject.Find("Card Manager").GetComponent<CardManager>();

        sign.transform.rotation = Camera.main.transform.rotation; // Causes the text faces camera.
        TextMesh tm = sign.AddComponent<TextMesh>();
        tm.text = cardManager.GetCurrentCardNameAsString();
        tm.color = Color.black;
        tm.fontStyle = FontStyle.Bold;
        tm.alignment = TextAlignment.Center;
        tm.anchor = TextAnchor.MiddleCenter;
        tm.characterSize = 0.065f;
        tm.fontSize = fontSize;
    }

    // Update is called once per frame
    void Update () {
        sign.transform.position = characterObject.transform.position + (Vector3.up * distanceAboveHead);
	}

    public void UpdateCurrentCard() {
        sign.GetComponent<TextMesh>().text = cardManager.GetCurrentCardNameAsString();
    }
}
