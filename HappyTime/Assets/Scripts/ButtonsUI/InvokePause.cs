using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokePause : MonoBehaviour {
    private GameObject pauseMenu;
    private GameObject UICanvas;
    public GameObject pausePrefab;

	void Start () {
        UICanvas = GameObject.FindGameObjectWithTag("UI");
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu == null)
        {
            pauseMenu = Instantiate(pausePrefab, UICanvas.transform);
        }
	}
}
