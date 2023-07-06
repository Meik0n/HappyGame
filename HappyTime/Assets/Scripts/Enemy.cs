 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float Health = 3f;

    public void TakeDamage(float Damage)
    {
        Health -= Damage;

        if(Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if(this.transform.parent.gameObject.GetComponent<Room>() != null)
        {
            this.transform.parent.gameObject.GetComponent<Room>().EnemiesInRoom.Remove(this.gameObject);
        }
        Destroy(gameObject);
    }
}
