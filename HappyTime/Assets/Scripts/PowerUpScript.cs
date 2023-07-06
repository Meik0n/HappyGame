using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Player>().AddLife(1))
            {
                Destroy(gameObject);
            }
        }
    }
}
