using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject pauseScreen;
    public AudioSource rain;
    private AudioSource source;
    public AudioClip click;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        TogglePauseScreen();
	}

    void TogglePauseScreen()
    {
        if(Input.GetKeyUp(KeyCode.P))
        {
            if(pauseScreen.activeSelf == false)
            {
                pauseScreen.SetActive(true);
                rain.Stop();
                source.PlayOneShot(click, 0.6f);
                Time.timeScale = 0f;
            }
            else
            {
                pauseScreen.SetActive(false);
                rain.Play();
                source.PlayOneShot(click, 0.6f);
                Time.timeScale = 1f;
            }
        }
    }
}
