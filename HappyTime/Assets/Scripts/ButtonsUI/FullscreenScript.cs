using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenScript : MonoBehaviour {

    private void Start()
    {
        if (Screen.fullScreen)
        {
            GetComponent<Toggle>().isOn = true;
        }
        else
        {
            GetComponent<Toggle>().isOn = false;
        }
    }


    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
