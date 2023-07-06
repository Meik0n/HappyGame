using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

    private void Start()
    {
        Pause();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnPause();
        }
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
}
