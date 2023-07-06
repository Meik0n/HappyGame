using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Seta : MonoBehaviour {

    public GameObject gas;
    public float AttackRate = 0.1f;
    private float TimeToAttack;

    void Start ()
    {
        TimeToAttack = Time.fixedTime + AttackRate;
    }

    private void Update()
    {
        if (Time.fixedTime >= TimeToAttack)
        {
            gameObject.GetComponent<Animator>().Play("Setilla", 0, 0.25f);
            Instantiate(gas, transform.position, Quaternion.identity);
            TimeToAttack = Time.fixedTime + AttackRate;
        }
    }
}
