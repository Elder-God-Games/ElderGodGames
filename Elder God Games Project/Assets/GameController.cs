using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject pauseScreen;
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

        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Initiate.Fade("TitleScreen", Color.black, 2f);
        }
	}

    void TogglePauseScreen()
    {
        if(Input.GetKeyUp(KeyCode.P))
        {
            if(pauseScreen.activeSelf == false)
            {
                pauseScreen.SetActive(true);
                source.PlayOneShot(click, 0.6f);
                Time.timeScale = 0f;
            }
            else
            {
                pauseScreen.SetActive(false);
                source.PlayOneShot(click, 0.6f);
                Time.timeScale = 1f;
            }
        }
    }

    //public void ResumeButton()
    //{
    //    pauseScreen.SetActive(false);
    //    source.PlayOneShot(click, 0.6f);
    //    Time.timeScale = 1f;
    //}
}
