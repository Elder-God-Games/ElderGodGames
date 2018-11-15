using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    // Properties
    public bool jumpAbility = true;

    public Rigidbody2D body;
    public Collider2D collider;

    public float speed;
    private float horizontalMove;

    public float jumpForce;
    public int noOfJumps;
    
    private int jumps;

    enum playerStates
    {
        IDLE,
        WALKING,
        ATTACKING,
        ASCENDING,
        DESCENDING
    }

    playerStates currentState;

    // Methods
    void Start()
    {
        jumps = 0;

        // Sets player's default state to idle
        // needs to be set back later
        currentState = playerStates.IDLE;
    }
    void Update()
    {

        Move();


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumps > 0 )
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
        // Set state to player walking (animate later)
        currentState = playerStates.WALKING;

        // Get Horizontal Axis movement and return either 1 or -1
        // multiply that by player speed to get your movement
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        body.velocity = new Vector2(horizontalMove, body.velocity.y);

    }
    void Jump()
    {
        // Set state to player jumping up (animate later)
        currentState = playerStates.ASCENDING;

        // Adds an upwards force to the player
        // takes away a jump from player's total jump count
        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumps--;

    }
    bool IsBelow()
    {
        if (collider.tag == "Platform")
        {
            if (collider.transform.position.y < body.transform.position.y)
            {
                return true;
            }
        }

        return false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player enters the collision field of the set collider
        if (collider.tag == "Platform")
        {
            // Make their total number of jumps equal to the number
            // of jumps specified in the designer
            jumps = noOfJumps;

            // Set their ability to jump to true
            jumpAbility = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Does nothing at the minute
        // just testing state switching
        currentState = playerStates.DESCENDING;
    }
}
