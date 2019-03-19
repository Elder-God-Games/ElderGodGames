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

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
            Initiate.Fade(ChangeTo, Color.black, 2f);
    }

    // Update is called once per frame
    void Update () {
        //if (this.GetComponent<BoxCollider2D>().IsTouching(collider))
        //{
        //    SceneManager.LoadScene(ChangeTo);
        //}
        
	}
}
