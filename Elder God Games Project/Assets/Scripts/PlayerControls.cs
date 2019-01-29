using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    // Properties
    public bool jumpAbility = true;

    public Rigidbody2D body;

    public float speed;
    private float horizontalMove;

    public float jumpForce;
    public int noOfJumps;
    
    private int jumps;
    
    // Methods
    void Start()
    {
        jumps = noOfJumps;
    }
    void Update()
    {

        Move();


        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
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
        else if(Input.GetKeyUp(KeyCode.Escape))
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
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player enters the collision field of the set collider
        if (collision.gameObject.tag == "Platform")
        {
            if (IsBelow(collision))
            {
                // Make their total number of jumps equal to the number
                // of jumps specified in the designer
                jumps = noOfJumps;

                // Set their ability to jump to true
                jumpAbility = true;

            }
        }
    }
    bool IsBelow(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            if (collision.transform.position.y < body.transform.position.y)
            {
                return true;
            }
        }

        return false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

    }
}
