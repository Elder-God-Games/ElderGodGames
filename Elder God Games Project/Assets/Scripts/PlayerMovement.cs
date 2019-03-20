using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public bool jumpAbility = true;

    public float speed;
    public float horizontalMove;
    private Rigidbody2D body;

    public float jumpForce;
    public int noOfJumps;
    private int jumps;

    // Use this for initialization
    void Start()
    {
        this.body = this.gameObject.GetComponent<Rigidbody2D>();
        
        jumps = noOfJumps;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
        // jump on Xbox controller currently mapped to 'A' 
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {

            // if the player still has jumps available
            if (jumps > 0)
            {
                // call the jump method
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

        // this is for changing the variable inside the animator.
        // if the player is moving it wil change a bool called Walking to true
        // else it will change the bool back to false and stop the waking animation
        // and send the player back to its idle animation. - Millsy
        if (horizontalMove > 0 || horizontalMove < 0)
        {
            gameObject.GetComponent<Animator>().SetBool("Walking", true);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("Walking", false);
        }
    }
    void Jump()
    {
        body.velocity = new Vector2(0, 0);

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
    /*
    private bool IsGrounded = false;


    private bool CheckGround(Vector3 pos)
    {
        return pos.y < transform.position.y;
    }
    */
}
