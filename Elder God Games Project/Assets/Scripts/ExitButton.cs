using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExitButton : MonoBehaviour, IPointerClickHandler
{
    public AudioClip note;
    private AudioSource source;

    private Vector3 Scale;
    private bool canPlay;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        Scale = this.transform.localScale;

        canPlay = true;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked Exit");

        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    private void OnMouseOver()
    {
        transform.localScale = new Vector3(0.9f, 0.9f, 9f);
        if (canPlay == true)
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
