using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class SceneButton : MonoBehaviour, IPointerClickHandler
{
    public string Scene;

    Vector2 Scale;

    [Range(0, 10)]
    public float multiplier = 2.0f;

    void Start()
    {
        Scale = this.transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        //SceneManager.LoadScene(Scene);
        Initiate.Fade(Scene, Color.black, multiplier);
    }

    void OnMouseOver()
    {
        Debug.Log("Mouse is Over");
        this.transform.localScale = new Vector2(-1, -1);
    }

    void OnMouseExit()
    {
        this.transform.localScale = Scale;
    }
   
}
