using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    public float timeToDestroy = 3f;
	void Start () {
        Destroy(this.gameObject, timeToDestroy);
	}
}
