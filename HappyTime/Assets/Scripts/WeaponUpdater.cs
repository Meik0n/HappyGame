using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpdater : MonoBehaviour {

    public Sprite Hoz;
    public Sprite Bullet;
    private Player pj;
    
    private void Start()
    {
        pj = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if(pj.CurrentWeapon == WeaponType.BULLETS)
        {
            GetComponent<Image>().sprite = Bullet;
        }
        else
        {
            GetComponent<Image>().sprite = Hoz;
        }
    }
}
