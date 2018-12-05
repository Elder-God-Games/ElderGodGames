using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAndMoveTowards : MonoBehaviour {
    // coliders for detection
    public CircleCollider2D playerCurcleColider;
    public BoxCollider2D boxColider;
    public GameObject questionMark;

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
    bool lostPlayer;

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
        boxColider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        questionMark.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // switch on the state machine
        switch (currentState)
        {
            
            case states.IDLE:
                #region
                //add proper idle timer.

                currentState = states.WALK;
                timer = Random.Range(3, 4);
                
                #endregion
                break;
            case states.WALK:

                if (!CanWalk)
                {
                    timer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        StartMoving();
                        questionMark.SetActive(false);
                    }
                }

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
        if (boxColider.IsTouching(playerCurcleColider))
        {
            int choice = 0;
            lostPlayer = false;
            transform.position = Vector2.MoveTowards(
                   transform.position,
                   new Vector2(playerCurcleColider.transform.position.x, transform.position.y),
                   speed * Time.deltaTime);

            // create a choice for dashing

            // decide to dash.
            choice = Random.Range(1, 2);
            if (choice == 2)
            {
                currentState = states.DASH;
            }
        }
        else if (!boxColider.IsTouching(playerCurcleColider) && lostPlayer == true)
        {
            LookAround();
        }
        else
        {
            lostPlayer = true;
        }
    }

    void LookAround()
    {
        StopMoving();
        questionMark.SetActive(true);

        Invoke("FlipSprite", 1f);
        Invoke("FlipSprite", .1f);
        Invoke("FlipSprite", 2.5f);
        Invoke("FlipSprite", 2f);
        currentState = states.IDLE;
    }
    public void Walk()
    {
        if (timer <= 0)
        {
            timer = Random.Range(2, 5);
        }
        rigidBody.velocity = (new Vector3(Direction.x * speed, rigidBody.velocity.y, 0));
        timer -= Time.deltaTime;

        // add in a wall detection.
            //bounce off the wall just by flipping the sprite

        if (timer <= 0)
        {
            StopMoving();
            NextWalk();
        }

        if (boxColider.IsTouching(playerCurcleColider))
        {
            currentState = states.WALKTOPLAYER;
        }
    }

    public void NextWalk()
    {
        int i = Random.Range(1, 10);

        if (i % 2 == 0)
        {
            FlipSprite();
            
        }
        StartMoving();
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


