using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public GameObject particulas;
    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.tag == "Enemy")
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(GameObject.Find("Player").GetComponent<Player>().bulletDamage); //acceder al valor del daño del player
            }
            else if (hit.GetComponent<HadaScript>() == true)
            {
                hit.GetComponent<HadaScript>().HurtFairy();
            }
        }

        if(hit.tag != "Player" && hit.tag !="NoCollide")
        {
            Instantiate(particulas, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
