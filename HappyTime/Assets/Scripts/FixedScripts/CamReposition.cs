using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CamReposition : MonoBehaviour {

    private Transform TargetRoomPos;

	void Update () {
        TargetRoomPos = GameObject.FindGameObjectWithTag("Player").transform.parent.transform;
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(TargetRoomPos.position.x, TargetRoomPos.position.y, -10), Time.deltaTime * 5);
	}
}
