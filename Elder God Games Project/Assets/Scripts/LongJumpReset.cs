using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongJumpReset : MonoBehaviour {

    BoxCollider2D boxCollider2D;
    public GameObject player;
    public Vector2 spawnPoint;
	void Start () {
        boxCollider2D = this.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (boxCollider2D.IsTouching(player.GetComponent<CircleCollider2D>()))
        {
            player.transform.position = spawnPoint;
        }
	}

    
}
