using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogEnemy : Enemy
{
    public GameObject Projectile;
    public Transform firePoint;
    private float timeBtwShoots;
    public float startTimeBtwShoots;
    private Transform target;

    private bool facingRight = false;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timeBtwShoots = startTimeBtwShoots;
    }


    void Update()
    {
        if (timeBtwShoots <= 0)
        {
            Instantiate(Projectile, firePoint.position, Quaternion.identity);
            timeBtwShoots = startTimeBtwShoots;
        }
        else
        {
            timeBtwShoots -= Time.deltaTime;
        }

        if(target.transform.position.x < transform.position.x)
        {
            if (facingRight)
            {
                Flip();
            }
        }
        else if(target.transform.position.x >= transform.position.x)
        {
            if (!facingRight)
            {
                Flip();
            }
            
        }
    }

    void Flip()
    {
       transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }

}
