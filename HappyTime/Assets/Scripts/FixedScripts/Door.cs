using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public bool isOpen = false;

    void Update()
    {
        if (isOpen)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            this.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }
    public void OpenDoor()
    {
        isOpen = true;
    }

    public void CloseDoor()
    {
        isOpen = false;
    }
}
