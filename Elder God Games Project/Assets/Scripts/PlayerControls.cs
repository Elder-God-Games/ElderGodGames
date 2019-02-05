using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    // Properties
    public bool jumpAbility = true;
    
    public float speed;
    private float horizontalMove;
    private Rigidbody2D body;

    public float jumpForce;
    public int noOfJumps;
    private int jumps;

    // Methods
    void Start()
    {
        this.body = this.gameObject.GetComponent<Rigidbody2D>();
        jumps = noOfJumps;
    }
    void Update()
    {

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

        if (Input.GetKeyDown(KeyCode.E))
        {
            Fire();
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Initiate.Fade("TitleScreen", Color.black, 1f);
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
    void Fire()
    {
        
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
