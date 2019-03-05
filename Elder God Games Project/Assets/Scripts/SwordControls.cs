using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordControls : MonoBehaviour {

    private CapsuleCollider2D weapon;
    float timer;

	// Use this for initialization
	void Start () {
        weapon = GetComponent<CapsuleCollider2D>();

        // time until weapon turns itself off
        timer = 6f;

        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        
        // decrement timer variable by one every update
        timer--;

        // if the timer variable reaches 0
        if (timer == 0)
        {
            // reset timer
            // set object to inactive
            timer = 6f;
            this.gameObject.SetActive(false);
        }
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if the collision is with an object timed enemy
        if (collision.gameObject.tag == "Enemy")
        {
            // destroy the enemy
            Destroy(collision.gameObject);
        }
    }
}
