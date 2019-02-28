using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public GameObject PauseScreen;
    public AudioClip click;
    private AudioSource source;

    private bool canPlay;
    [Range(1, 4)]
    public int buttonNumber;

    private enum ButtonState
    {
        Resume,
        Restart,
        Leaderboard,
        Exit
    }

    ButtonState buttonState;

    Vector2 Scale;

    void Start()
    {
        Scale = transform.localScale;
        source = GetComponent<AudioSource>();
        canPlay = true;

        switch(buttonNumber)
        {
            case 1:
                buttonState = ButtonState.Resume;
                break;
            case 2:
                buttonState = ButtonState.Restart;
                break;
            case 3:
                buttonState = ButtonState.Leaderboard;
                break;
            case 4:
                buttonState = ButtonState.Exit;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {

    } 

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        source.PlayOneShot(click);
        switch (buttonState)
        {
            case ButtonState.Resume:
                PauseScreen.SetActive(false);
                break;

            case ButtonState.Restart:
                Initiate.Fade(SceneManager.GetActiveScene().name,
                    Color.black,
                    2.0f);
                break;

            case ButtonState.Leaderboard:
                break;
            case ButtonState.Exit:
                Initiate.Fade("TitleScreen",
                    Color.black,
                    2.0f);
                break;
        }
    }

    private void OnMouseOver()
    {
        transform.localScale = new Vector2(0.9f, 0.9f);
        Debug.Log("Mouse is over");
        if (canPlay == true)
        {
            if (!source.isPlaying)
            {
                canPlay = false;
            }
        }
    }

    void OnMouseExit()
    {
        Debug.Log("Mouse exit");
        transform.localScale = Scale;
        canPlay = true;
    }

    
}
