using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panda : MonoBehaviour {

    public GameObject deathEffect;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        gameObject.transform.parent.GetComponent<Room>().EnemiesInRoom.Remove(gameObject);
    }
}
