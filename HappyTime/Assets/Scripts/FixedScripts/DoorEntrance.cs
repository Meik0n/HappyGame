using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum direction {up, down, left, right};

public class DoorEntrance : MonoBehaviour {
    public GameObject Exit;
    public direction ExitDirection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && this.transform.parent.GetComponent<Door>().isOpen == true)
        {
            switch (ExitDirection)
            {
                case direction.left:
                    collision.gameObject.transform.position = new Vector3(Exit.transform.position.x - 1f, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
                    break;
                case direction.right:
                    collision.gameObject.transform.position = new Vector3(Exit.transform.position.x + 1f, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
                    break;
                case direction.down:
                    collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x, Exit.transform.position.y - 1f, collision.gameObject.transform.position.z);
                    break;
                case direction.up:
                    collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x, Exit.transform.position.y + 1f, collision.gameObject.transform.position.z);
                    break;
            }
        }
    }
}
