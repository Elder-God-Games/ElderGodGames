using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToInputScene : MonoBehaviour {

    public string ChangeTo;
    public CircleCollider2D collider;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (this.GetComponent<BoxCollider2D>().IsTouching(collider))
        {
            SceneManager.LoadScene(ChangeTo);
        }
        
	}

    public void ChangeToProvidedScene()
    {
        Initiate.Fade("LevelOne",Color.black,.2f);
    }

}
