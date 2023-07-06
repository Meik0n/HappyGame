using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour {

	void Start () {
        GetComponent<Slider>().value = AudioListener.volume;
    }
	
	void Update () {
        AudioListener.volume = GetComponent<Slider>().value;
    }
}
