using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    public List<GameObject> DoorsInRoom = new List<GameObject>();
    public List<GameObject> EnemiesInRoom = new List<GameObject>();
    public bool Active = false;
    public bool isEmpty = false;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            if(child.gameObject.tag == "Enemy")
            {
                EnemiesInRoom.Add(child.gameObject);
            }
        }
    }

    void Update () {
        if (EnemiesInRoom.Count > 0)
        {
            isEmpty = false;
        }
        else
        {
            isEmpty = true;
        }
        if (isEmpty == true)
        {
            if(Active)
            {
                foreach (GameObject d in DoorsInRoom)
                {
                    d.GetComponent<Door>().OpenDoor();
                }
            }
        }
        else
        {
            if(Active)
            {
                foreach (GameObject d in DoorsInRoom)
                {
                    d.GetComponent<Door>().CloseDoor();
                }
                foreach (GameObject e in EnemiesInRoom)
                {
                    if (e.GetComponent<DisableInRoom>() != null)
                    {
                        e.GetComponent<DisableInRoom>().IsEnabled = true;
                    }
                    e.gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (GameObject e in EnemiesInRoom)
                {
                    if (e.GetComponent<DisableInRoom>() != null)
                    {
                        e.GetComponent<DisableInRoom>().IsEnabled = false;
                    }
                    e.gameObject.SetActive(false);
                }
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Active = true;
            collision.gameObject.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Active = false;
        }
    }
}
