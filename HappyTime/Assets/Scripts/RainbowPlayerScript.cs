using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowPlayerScript : MonoBehaviour {

    private Transform Target = null;
    public float speed = 10;
    public GameObject emplosion;

	void Start () {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update () {
        gameObject.GetComponent<Rigidbody2D>().AddForce((Target.position - transform.position).normalized * 5); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().LooseLife(1);
        }
        else if(collision.gameObject.tag == "Obstacle")
        {
            Instantiate(emplosion, transform.position, Quaternion.identity);
            gameObject.GetComponent<ParticleSystem>().Stop();
            Destroy(GetComponent<CircleCollider2D>());
            Destroy(gameObject, 2);
        }
    }
}