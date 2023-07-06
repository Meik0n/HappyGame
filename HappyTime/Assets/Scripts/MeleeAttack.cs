using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {
    private int damage;

    private void Awake()
    {
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().MeleeDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if(collision.gameObject.GetComponent<Enemy>() == true)
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
            else if(collision.gameObject.GetComponent<HadaScript>() == true)
            {
                collision.gameObject.GetComponent<HadaScript>().HurtFairy();
            }
        }
    }
}
