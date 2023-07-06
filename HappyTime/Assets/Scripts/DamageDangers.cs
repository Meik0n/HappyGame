using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDangers : MonoBehaviour {

    public int ContactDamage = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().LooseLife(ContactDamage);
            Debug.Log("Bump");
        }
    }
}
