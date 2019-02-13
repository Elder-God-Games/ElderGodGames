using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public bool jumpAbility = true;

    public float speed;
    private float horizontalMove;
    private Rigidbody2D body;

    public float jumpForce;
    public int noOfJumps;
    private int jumps;

    // Use this for initialization
    void Start () {
        this.body = this.gameObject.GetComponent<Rigidbody2D>();

        speed = 3;
        noOfJumps = 2;
        jumps = noOfJumps;
    }
	
	// Update is called once per frame
	void Update () {
        Move();


        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            if (jumps > 0)
            {
                Jump();
            }
            else
            {
                // Else turn off their ability to jump
                jumpAbility = false;
            }
        }
    }
    void Move()
    {
        // Get Horizontal Axis movement and return either 1 or -1
        // multiply that by player speed to get your movement
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        body.velocity = new Vector2(horizontalMove, body.velocity.y);

    }
    void Jump()
    {
        // Adds an upwards force to the player
        // takes away a jump from player's total jump count
        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumps--;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player enters the collision field of the set collider
        if (collision.gameObject.tag == "Platform")
        {

            // Make their total number of jumps equal to the number
            // of jumps specified in the designer
            jumps = noOfJumps;

            // Set their ability to jump to true
            jumpAbility = true;

        }
    }
}
