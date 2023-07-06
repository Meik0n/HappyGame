using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Animations;

public enum Direction { UP, DOWN, LEFT, RIGHT };
public enum WeaponType { HOZ, BULLETS};

public struct dashPower{
    public float actualValue;
    public float defaultValue;

    public dashPower(float v)
    {
        defaultValue = v;
        actualValue = v;
    }
}

public class Player : MonoBehaviour {

    //Movement
    private Rigidbody2D rb2d;
    public float speed = 5f;
    float moveHorizontal;
    float moveVertical;
    public Sprite sprite;
    private Vector2 playerPosition;

    //Dash
    private dashPower dashDecrease = new dashPower(5);
    public int MaxDash = 3;
    public int ActualDashes;
    public bool CanDash = false;
    private float TimeToDash;
    private float DashRate = 2f;

    //Bullets
    public GameObject bulletPrefab;
    private Direction LookingAt = Direction.DOWN;
    private bool IsShooting = false;
    private float TimeToShoot;
    public float bulletDamage = 1f;
    public float ShootSpeed = 5f;
    public float ShootRate = 0.25f;
    public float BulletDestroyTime = 2f;

    //Scitche
    public GameObject UpCollider;
    public GameObject DownCollider;
    public GameObject LeftCollider;
    public GameObject RightCollider;
    private float TimeToMelee = 0.1f;
    public int MeleeDamage = 2;

    //Life
    public int MaxLife = 3;
    public int CurrentLife = 3;
    public bool Invulnerability = false;
    public GameObject GameOver;
    private float BlinkingTime = 0;

    //Weapon
    public WeaponType CurrentWeapon = WeaponType.BULLETS;

    public IEnumerator StartInvulnerability()
    {
        Invulnerability = true;
        yield return new WaitForSeconds(1);
        Invulnerability = false;
    }

    public IEnumerator RefillDash()
    {
        while (true)
        {
            if (ActualDashes < MaxDash)
            {
                if (Time.fixedTime >= TimeToDash)
                {
                    AddDash();
                    TimeToDash = Time.fixedTime + DashRate;
                }
            }
            yield return null;
        }
    }

    public IEnumerator DoTheMelee()
    {

        if (LookingAt == Direction.UP)
        {
            UpCollider.gameObject.SetActive(true);
            DownCollider.gameObject.SetActive(false);
            LeftCollider.gameObject.SetActive(false);
            RightCollider.gameObject.SetActive(false);
            yield return new WaitForSeconds(TimeToMelee);
            RightCollider.gameObject.SetActive(false);
            UpCollider.gameObject.SetActive(false);
            DownCollider.gameObject.SetActive(false);
            LeftCollider.gameObject.SetActive(false);
        }
        else if (LookingAt == Direction.DOWN)
        {
            DownCollider.gameObject.SetActive(true);
            UpCollider.gameObject.SetActive(false);
            LeftCollider.gameObject.SetActive(false);
            RightCollider.gameObject.SetActive(false);
            yield return new WaitForSeconds(TimeToMelee);
            RightCollider.gameObject.SetActive(false);
            UpCollider.gameObject.SetActive(false);
            DownCollider.gameObject.SetActive(false);
            LeftCollider.gameObject.SetActive(false);
        }
        else if (LookingAt == Direction.LEFT)
        {
            LeftCollider.gameObject.SetActive(true);
            UpCollider.gameObject.SetActive(false);
            DownCollider.gameObject.SetActive(false);
            RightCollider.gameObject.SetActive(false);
            yield return new WaitForSeconds(TimeToMelee);
            RightCollider.gameObject.SetActive(false);
            UpCollider.gameObject.SetActive(false);
            DownCollider.gameObject.SetActive(false);
            LeftCollider.gameObject.SetActive(false);
        }
        else if (LookingAt == Direction.RIGHT)
        {
            RightCollider.gameObject.SetActive(true);
            UpCollider.gameObject.SetActive(false);
            DownCollider.gameObject.SetActive(false);
            LeftCollider.gameObject.SetActive(false);
            yield return new WaitForSeconds(TimeToMelee);
            RightCollider.gameObject.SetActive(false);
            UpCollider.gameObject.SetActive(false);
            DownCollider.gameObject.SetActive(false);
            LeftCollider.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        ActualDashes = MaxDash;
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(RefillDash());
    }

    void FixedUpdate()
    {
        playerPosition = transform.position;
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        //Ejecutar Dash
        if (CanDash)
        {
            Dash();
        }

        //Actualizar movimiento
        rb2d.velocity = new Vector2(speed * moveHorizontal, speed * moveVertical);
        if(rb2d.velocity.magnitude == 0)
        {
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("isWalking", false);
        }
        else
        {
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("isWalking", true);
        }
    }

    void Update()
    {
        //Keys
        if (Input.GetKeyDown(KeyCode.R))
        {
            CurrentWeapon = CurrentWeapon == WeaponType.BULLETS ? WeaponType.HOZ : WeaponType.BULLETS;
        }
        if(CurrentWeapon == WeaponType.BULLETS)
        {
            //NO TOCAR
            if (Input.GetButton("FireUp"))
            {
                LookingAt = Direction.UP;
                IsShooting = true;
            }
            else if (Input.GetButton("FireDown"))
            {
                LookingAt = Direction.DOWN;
                IsShooting = true;
            }
            else if (Input.GetButton("FireLeft"))
            {
                LookingAt = Direction.LEFT;
                IsShooting = true;
            }
            else if (Input.GetButton("FireRight"))
            {
                LookingAt = Direction.RIGHT;
                IsShooting = true;
            }
            else
            {
                IsShooting = false;
            }
            //Ejecutar Shoot
            if (IsShooting)
            {
                if (Time.fixedTime >= TimeToShoot)
                {
                    Shoot();
                    TimeToShoot = Time.fixedTime + ShootRate;
                }
            }
        }
        else if (CurrentWeapon == WeaponType.HOZ)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                LookingAt = Direction.UP;
                StartCoroutine(DoTheMelee());
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                LookingAt = Direction.DOWN;
                StartCoroutine(DoTheMelee());
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                LookingAt = Direction.LEFT;
                StartCoroutine(DoTheMelee());
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                LookingAt = Direction.RIGHT;
                StartCoroutine(DoTheMelee());
            }

            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                LookingAt = Direction.UP;
                StopCoroutine(DoTheMelee());
                RightCollider.gameObject.SetActive(false);
                UpCollider.gameObject.SetActive(false);
                DownCollider.gameObject.SetActive(false);
                LeftCollider.gameObject.SetActive(false);
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                LookingAt = Direction.DOWN;
                StopCoroutine(DoTheMelee());
                RightCollider.gameObject.SetActive(false);
                UpCollider.gameObject.SetActive(false);
                DownCollider.gameObject.SetActive(false);
                LeftCollider.gameObject.SetActive(false);
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                LookingAt = Direction.LEFT;
                StopCoroutine(DoTheMelee());
                RightCollider.gameObject.SetActive(false);
                UpCollider.gameObject.SetActive(false);
                DownCollider.gameObject.SetActive(false);
                LeftCollider.gameObject.SetActive(false);
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                LookingAt = Direction.RIGHT;
                StopCoroutine(DoTheMelee());
                RightCollider.gameObject.SetActive(false);
                UpCollider.gameObject.SetActive(false);
                DownCollider.gameObject.SetActive(false);
                LeftCollider.gameObject.SetActive(false);
            }

        }

        //LLamar al dash
        if (Input.GetButton("Dash") && ActualDashes > 0)
        {
            gameObject.GetComponent<TrailRenderer>().enabled = true;
            CanDash = true;
            LooseDash();
        }

        //Ejecutar Invulnerabilidad
        if (Invulnerability)
        {
            if(Time.fixedTime >= BlinkingTime)
            {
                BlinkingTime = Time.fixedTime + 0.10f;
                if(this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled == true)
                {
                    this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
        else
        {
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        }

        //HADA
        foreach(GameObject g in gameObject.transform.parent.GetComponent<Room>().EnemiesInRoom)
        {
            if(g.tag == "Enemy" && g.GetComponent<HadaScript>() == true)
            {
                g.SetActive(true);
            }
        }
    }

    private void Shoot()
    {

        Vector2 bulletPosition = playerPosition;
        GameObject Bullet = null;

        if (LookingAt == Direction.UP)
        {
            bulletPosition.y += sprite.bounds.extents.y;
            Bullet = (GameObject)Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);
            Bullet.GetComponent<Rigidbody2D>().velocity = Bullet.transform.up * ShootSpeed;
        }
        else if (LookingAt == Direction.DOWN)
        {
            bulletPosition.y -= sprite.bounds.extents.y;
            Bullet = (GameObject)Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);
            Bullet.GetComponent<Rigidbody2D>().velocity = -Bullet.transform.up * ShootSpeed;
        }
        else if (LookingAt == Direction.LEFT)
        {
            bulletPosition.x -= sprite.bounds.extents.x;
            Bullet = (GameObject)Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);
            Bullet.GetComponent<Rigidbody2D>().velocity = -Bullet.transform.right * ShootSpeed;
        }
        else if (LookingAt == Direction.RIGHT)
        {
            bulletPosition.x += sprite.bounds.extents.x;
            Bullet = (GameObject)Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);
            Bullet.GetComponent<Rigidbody2D>().velocity = Bullet.transform.right * ShootSpeed;
        }

        Destroy(Bullet, BulletDestroyTime);
    }

    public void Dash()
    {
        if (dashDecrease.actualValue > 1)
        {
            moveHorizontal *= dashDecrease.actualValue;
            moveVertical *= dashDecrease.actualValue;
            dashDecrease.actualValue *= Time.deltaTime * 45;
        }
        else
        {
            gameObject.GetComponent<TrailRenderer>().enabled = false;
            dashDecrease.actualValue = dashDecrease.defaultValue;
            CanDash = false;
        }
    }

    public void AddDash()
    {
        ActualDashes++;
    }

    public void LooseDash()
    {
        TimeToDash = Time.fixedTime + DashRate;
        ActualDashes--;
    }

    public void LooseLife(int LifeToLoose)
    {
        if(Invulnerability == false)
        {
            if (CurrentLife - LifeToLoose > 0)
            {
                CurrentLife -= LifeToLoose;
                StartCoroutine(StartInvulnerability());
            }
            else
            {
                CurrentLife = 0;
            }
        }
        if(CurrentLife < 1)
        {
            Die();
        }
    }

    public bool AddLife(int LifeToAdd)
    {
        if(LifeToAdd + CurrentLife <= MaxLife)
        {
            CurrentLife += LifeToAdd;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Die()
    {
        Instantiate(GameOver, GameObject.FindGameObjectWithTag("UI").transform);
        Time.timeScale = 0;
    }
}
