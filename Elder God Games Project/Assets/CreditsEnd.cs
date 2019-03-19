using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsEnd : MonoBehaviour {

    AudioSource source;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!source.isPlaying)
        {
            Initiate.Fade("TitleScreen", Color.black, 2f);
        }
        else if(Input.GetKeyUp(KeyCode.Return))
        {
            Initiate.Fade("TitleScreen", Color.black, 2f);
        }
    }
}
