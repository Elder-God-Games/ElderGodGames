﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public GameObject Owner;
    public float force;

    Rigidbody2D body;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();

        body.AddForce(this.transform.forward * force, ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {

        
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {

        }
        else
        {
            Destroy(this);
        }
    }
}
