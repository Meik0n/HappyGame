using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyUpdate : MonoBehaviour {
    int CandyNumber;
    float UnitWidth;

    void Start()
    {
        UnitWidth = this.GetComponent<RectTransform>().sizeDelta.x / 3;
    }

    void Update()
    {
        CandyNumber = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentLife;
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(UnitWidth * CandyNumber, this.GetComponent<RectTransform>().sizeDelta.y);
    }
}
