using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    // Properties
    public Rigidbody2D body;
    public Collider2D collider;
    public float speed;
    public bool jumpAbility = true;
    public float jumpForce;
    public int noOfJumps = 2;

    private float horizontalMove;
    private int jumps;
    
    // Methods
    void Start() {
        jumps = 0;
    }
    void Update() {

        //Debug.Log(Input.GetAxis("Horizontal"));
        //Debug.Log(Input.GetAxis("Vertical"));
        
    }
    void FixedUpdate() {

        Move();

        if (body.IsTouching(collider))
        {
            jumps = noOfJumps;
            jumpAbility = true;
        }

        if (jumpAbility)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (jumps > 0)
                {
                    Jump();
                }
                else
                {
                    jumpAbility = false;
                }
            }
        }
    }
    void Move()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        body.velocity = new Vector2(horizontalMove, body.velocity.y);
    }
    void Jump(){

        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumps--;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
