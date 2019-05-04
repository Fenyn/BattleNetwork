using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public CardManager cardManager;
    public float distanceAboveHead = 1f;
    public int fontSize = 50;

    List<GameObject> signs;

    private List<Unit> objectsWithHP;

    void Awake() {
        objectsWithHP = new List<Unit>();
        signs = new List<GameObject>();
        cardManager = GameObject.Find("Card Manager").GetComponent<CardManager>();
        //AddObjectWithHP(GameObject.Find("Player"));
    }

    // Update is called once per frame
    void Update () {
        foreach (Unit unit in objectsWithHP) {
            int listIndex = objectsWithHP.IndexOf(unit);
            signs[listIndex].transform.position = unit.transform.position + (Vector3.up * distanceAboveHead);
            signs[listIndex].GetComponent<TextMesh>().text = unit.CurrentHealth.ToString();
        }
        //sign.transform.position = characterObject.transform.position + (Vector3.up * distanceAboveHead);
	}

    public void UpdateCurrentCard() {
        signs[0].GetComponent<TextMesh>().text = cardManager.GetCurrentCardNameAsString();
    }

    public void AddObjectWithHP(Unit unit) {
        objectsWithHP.Add(unit);

        GameObject text_go = new GameObject(unit.name.ToLower() + "_label");
        text_go.transform.rotation = Camera.main.transform.rotation; // Causes the text to face the camera.
        text_go.transform.SetParent(unit.transform);

        TextMesh tm = text_go.AddComponent<TextMesh>();
        tm.text = unit.CurrentHealth.ToString();
        tm.color = Color.black;
        tm.fontStyle = FontStyle.Bold;
        tm.alignment = TextAlignment.Center;
        tm.anchor = TextAnchor.MiddleCenter;
        tm.characterSize = 0.065f;
        tm.fontSize = fontSize;
        signs.Add(text_go);

    }

    public void RemoveObjectWithHP(Unit unit) {
        GameObject text_go = signs[objectsWithHP.IndexOf(unit)];
        signs.Remove(text_go);
        Destroy(text_go);
        objectsWithHP.Remove(unit);
    }
}
