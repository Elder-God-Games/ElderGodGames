using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FireballTargeter : MonoBehaviour
{

    public Transform player;

    public float speed = 2f;
    public float rotateSpeed = 500f;
    public float lifeSpan = 3f;
    public GameObject Mage;
    public GameObject Fireball;
    new Vector3 defaultPosition;

    private Rigidbody2D rigidbody;

    void Start()
    {
        defaultPosition = Mage.transform.position;
        Fireball.SetActive(false);
        Fireball.GetComponent<FireballTargeter>().enabled = false;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        defaultPosition = Mage.transform.position;
        Fireball.GetComponent<FireballTargeter>().enabled = true;
        lifeSpan -= Time.deltaTime;
        if (lifeSpan < 0)
        {
            Fireball.SetActive(false);
            resetFireball();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Deal damage to player 
            Fireball.SetActive(false);
            resetFireball();
        }

        if (col.gameObject.tag == "Platform")
        {
            //Deal damage to player 
            Fireball.SetActive(false);
            resetFireball();
        }
    }

    public void resetFireball() //Resets the fireball
    {
        lifeSpan = 6f;
        transform.position = defaultPosition;
    } 

    void FixedUpdate() //Moves the Fireball
    {
        Vector2 direction = (Vector2)player.position - rigidbody.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rigidbody.angularVelocity = -rotateAmount * rotateSpeed;

        rigidbody.velocity = transform.up * speed;
    }
}
