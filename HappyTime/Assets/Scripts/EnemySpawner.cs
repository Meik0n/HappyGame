using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject EnemyToSpawn;
    private Vector2 SpawnerPosition;
    public float SpawnRate = 5f;
    private float TimeToSpawn;

    void Start ()
    {
        SpawnerPosition = this.transform.position;
        TimeToSpawn = Time.fixedTime + SpawnRate;
    }

    private void Update()
    {
        if (Time.fixedTime >= TimeToSpawn)
        {
            GameObject TheEnemy = (GameObject)Instantiate(EnemyToSpawn, SpawnerPosition, Quaternion.identity);
            TheEnemy.transform.parent = this.transform.parent;
            TimeToSpawn = Time.fixedTime + SpawnRate;
        }
    }
}
