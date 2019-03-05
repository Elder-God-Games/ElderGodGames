using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    
    public float force;
    private Rigidbody2D body;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();

        body.AddForce(this.transform.forward * force, ForceMode2D.Impulse);

        this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {

        
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(this);
        }
        else
        {
            Destroy(this);
        }
    }
}
