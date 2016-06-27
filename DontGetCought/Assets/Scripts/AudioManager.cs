using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public AudioSource backgroundMusik;
    public AudioClip musik;


	// Use this for initialization
	void Start () {
        backgroundMusik.clip = musik;
        backgroundMusik.loop = true;
        backgroundMusik.Play();
	}

    /*
    void setAudioClip(AudioSource source, string clip) {
        clip
        source.clip = 
    }
    */
}
