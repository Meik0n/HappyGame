using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt2 : MonoBehaviour {
    public int ContactDamage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().LooseLife(ContactDamage);
        }
    }
}
