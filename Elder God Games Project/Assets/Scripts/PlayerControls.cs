using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    // Properties
    public Rigidbody2D body;
    public float speed;
    public float jumpForce;
    private float horizontalMove;
    public bool jumpAbility = true;
    private int noOfJumps = 2;

    // Methods
    void Start() {
        
    }
    void Update() {

        Debug.Log(Input.GetAxis("Horizontal"));
        Debug.Log(Input.GetAxis("Vertical"));
    }
    void FixedUpdate() {

        Move();

        if (jumpAbility)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
    }
    void Move() {

        horizontalMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.A)) 
        {
            body.velocity = new Vector2(horizontalMove * speed, body.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            body.velocity = new Vector2(horizontalMove * speed, body.velocity.y);
        }
    }
    void Jump(){

        body.AddForce(Vector2.up * jumpForce);
    }
}
