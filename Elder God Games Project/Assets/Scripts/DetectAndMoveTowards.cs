using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAndMoveTowards : MonoBehaviour {
    // coliders for detection
    public CircleCollider2D playerCurcleColider;
    public BoxCollider2D boxColider;

    // attack ability trigger
    public bool attackAbility;
    public float AttackDistance;
    float distance;

    public bool CanDash;
    public bool CanWalk = true;

    //walk timer var
    public float timer;

    public float speed;
    // a sprite to see the point that the enemy will dash too for debuging
    // the count to make sure it only creates one target
    private Rigidbody2D rigidBody;
    private Vector3 Direction = Vector3.left;
    SpriteRenderer renderer;

    // state machine
    enum states
    {
        IDLE,
        WALK,
        WALKTOPLAYER,
        ATTACK,
        DASH
    }
    states currentState;
    public float dashDelayTime;

    // Use this for initialization
    void Start()
    {
        currentState = states.IDLE;
        rigidBody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        // switch on the state machine
        switch (currentState)
        {
            
            case states.IDLE:
                #region
                currentState = states.WALK;
                #endregion
                break;
            case states.WALK:
                if (CanWalk)
                {
                    Walk();
                }
                break;
            case states.WALKTOPLAYER:
                WalkTowardsPlayer();

                break;
            case states.ATTACK:
                Attack();
                
                break;
            case states.DASH:
                Dash();
                
                break;
            default:
                break;
        }

        
    }
    public void Dash()
    {
        
    }
    public void Attack()
    {
        // creates a new box colider that is a trigger
        BoxCollider2D attackCollider = this.gameObject.AddComponent<BoxCollider2D>();
        attackCollider.isTrigger = true;
        // tests o see if the player is left or right of the enemy
        // sets the offset based on this.
        if (transform.position.x > playerCurcleColider.transform.position.x)
        {
            attackCollider.offset = new Vector2(-0.16f, 0.05f);
        }
        else if (transform.position.x < playerCurcleColider.transform.position.x)
        {
            attackCollider.offset = new Vector2(0.16f, 0.05f);
        }
        // turn off the attack colider or a memory leak exists.
        attackAbility = !attackAbility;
    }
    public void WalkTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(
                    transform.position,
                    new Vector2(playerCurcleColider.transform.position.x, transform.position.y),
                    speed * Time.deltaTime);
    }
    public void Walk()
    {
        if (timer <= 0)
        {
            timer = Random.Range(2, 5);
        }
        rigidBody.velocity = (new Vector3(Direction.x * speed, rigidBody.velocity.y, 0));
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            CanWalk = false;
        }
    }
    public void WalkTimer(float wait)
    {
        
        

    }

    public void FlipSprite()
    {
        //for flipping the player sprite and colider
        Direction = -Direction;
        renderer.flipX = !renderer.flipX;
        boxColider.offset = new Vector2(-boxColider.offset.x, boxColider.offset.y);
    }

    public void StartMoving()
    {
        CanWalk = true;
    }
    public void StopMoving()
    {
        CanWalk = false;
    }
}


