using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControls : MonoBehaviour {

    private CapsuleCollider2D weapon;
    float timer;

	// Use this for initialization
	void Start () {
        weapon = GetComponent<CapsuleCollider2D>();

        timer = 6f;

        this.gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {

        timer--;

        if (timer == 0)
        {
            this.gameObject.SetActive(false);
            timer = 6f;
        }
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
