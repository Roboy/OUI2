using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeControl : MonoBehaviour {
    private CustomSlider slider;
    private AudioSource song;
    
    // Start is called before the first frame update
    void Start() {
        slider = GameObject.FindObjectOfType<CustomSlider>();
        song = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        song.volume = slider.GetValue();
    }
}
