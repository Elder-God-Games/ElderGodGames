using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SplashScreenController : MonoBehaviour
{
    public AudioClip jingle;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start ()
    {
        source.PlayOneShot(jingle, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
        {
            Initiate.Fade("TitleScreen", Color.black, 2.0f);
        }
    }
}
