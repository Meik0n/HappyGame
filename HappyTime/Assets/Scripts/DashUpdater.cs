using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashUpdater : MonoBehaviour {
    int DashNumber;
    float UnitWidth;

	void Start () {
        UnitWidth = this.GetComponent<RectTransform>().sizeDelta.x / 3;
    }

	void Update () {
        DashNumber = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ActualDashes;
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(UnitWidth * DashNumber, this.GetComponent<RectTransform>().sizeDelta.y);
	}
}