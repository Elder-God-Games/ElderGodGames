using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResumeButton : MonoBehaviour {

    public GameObject PauseScreen;
    public AudioClip click;
    private AudioSource source;
    
    private bool canPlay;

    Vector2 Scale;
    // Use this for initialization
    void Start () {

        Scale = transform.localScale;
        source = GetComponent<AudioSource>();
        canPlay = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        source.PlayOneShot(click);
    }

    private void OnMouseOver()
    {
        transform.localScale = new Vector2(0.9f, 0.9f);
        Debug.Log("Mouse is over");
    }

    void OnMouseExit()
    {
        Debug.Log("Mouse exit");
        transform.localScale = Scale;
    }
}
