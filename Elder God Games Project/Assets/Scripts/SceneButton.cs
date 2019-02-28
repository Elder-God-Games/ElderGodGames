using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour, IPointerClickHandler
{
    public string Scene;
    public AudioClip note, click;
    private AudioSource source;

    private bool canPlay;

    Vector2 Scale;

    [Range(0, 10)]
    public float multiplier = 2.0f;

    void Start()
    {
        Scale = this.transform.localScale;
        source = GetComponent<AudioSource>();
        canPlay = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        source.PlayOneShot(click);
        //SceneManager.LoadScene(Scene);
        Initiate.Fade(Scene, Color.black, multiplier);
    }

    private void OnMouseOver()
    {
        transform.localScale = new Vector3(0.9f, 0.9f, 9f);
        if(canPlay == true)
        {
            if (!source.isPlaying)
            {
                source.PlayOneShot(note, 0.7f);
                canPlay = false;
            }
        }
    }

    void OnMouseExit()
    {
        this.transform.localScale = Scale;
        canPlay = true;
    }
   
}
