using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {
    public GameObject mosterPrefab;

	void Update () {
		if(GetComponent<DisableInRoom>().IsEnabled == true)
        {
            var enemy = Instantiate(mosterPrefab, transform.position, Quaternion.identity);
            enemy.transform.parent = transform.parent;
            transform.parent.GetComponent<Room>().EnemiesInRoom.Remove(gameObject);
            transform.parent.GetComponent<Room>().EnemiesInRoom.Add(enemy);
            Destroy(gameObject);
        }
	}
}
