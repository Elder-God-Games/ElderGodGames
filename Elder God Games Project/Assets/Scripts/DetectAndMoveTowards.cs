using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAndMoveTowards : MonoBehaviour {
    // coliders for detection
    public CircleCollider2D playerCurcleColider;
    public CircleCollider2D collider;
    public BoxCollider2D boxColider;
    public GameObject questionMark;
    public GameObject exclimationMark;
    GameObject EnemyTurner;

    // attack ability trigger
    public bool attackAbility;
    public float AttackDistance;
    float distance;

    public bool CanDash;
    public bool CanWalk = true;
    public bool Turned = false;

    //walk timer var
    public float timer1;
    public float timer2 = 2;
    public float timer3 = 2;

    public float speed;
    // a sprite to see the point that the enemy will dash too for debuging
    // the count to make sure it only creates one target
    private Rigidbody2D rigidBody;
    private Vector3 Direction = Vector3.left;
    SpriteRenderer renderer;
    bool lostPlayer;
    public float dashMutiplyer;
    [SerializeField]
    bool dashed;

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
    // all initializations for the cript to function
    void Start()
    {
        currentState = states.IDLE;
        playerCurcleColider = GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>();
        boxColider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<CircleCollider2D>();
        //EnemyTurner = GameObject.FindGameObjectWithTag("EnemyTurner");
        questionMark.SetActive(false);
        exclimationMark.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // switch on the state machine
        switch (currentState)
        {
            // the state of idle for the enemy, stops the enemy from moving and statrts walking
            // used for going between states and to reset timers.
            case states.IDLE:
                #region
                //add proper idle timer.

                currentState = states.WALK;
                timer1 = Random.Range(3, 4);
                timer2 = 2;
                
                #endregion
                break;
                // the walk state sets the player moving for a randomized amount of time
                // in the direction the sprite faces but alows the sprite to flip and change direction.
                // timers are used for the randomization as well as modulus quaries.
            case states.WALK:

                if (!CanWalk)
                {
                    timer1 -= Time.deltaTime;
                    if (timer1 <= 0)
                    {
                        StartMoving();
                        questionMark.SetActive(false);
                    }
                    if (boxColider.IsTouching(playerCurcleColider))
                    {
                        CancelInvoke();
                        timer1 = 0;
                    }
                }

                if (CanWalk)
                {
                    Walk();
                }

                break;
                // calls the walk towards player method that only chases the players x position.
            case states.WALKTOPLAYER:
                WalkTowardsPlayer();
                break;
                // attack state created in the early stages.
                // basis for the players attack method.
            case states.ATTACK:
                Attack();
                break;
                // changes to the dash state and call the dash method. this will make the enemy
                // dash in the direction it is facing and is only called when it can see the player.
            case states.DASH:
                Dash();
                break;
            default:
                break;
        }

        
    }
    // the dash method creates an exclimationmark to show what is happening,
    // this mark grows and shrinks over 3 seconds and then deactivates itself
    // then the dash happens applying force in the direction it is facing and
    // a small amount of fource on the y axis to give the enemy some lift
    // to avoide the friction of its colider agenst the ground colider
    public void Dash()
    {
        // cause an ! to be created as a timer before dash.
        timer3 -= Time.deltaTime;
        exclimationMark.SetActive(true);
        if (timer3 <= 0)
        {
            exclimationMark.SetActive(false);

            if (CanDash)
            {
                rigidBody.AddForce((Direction * dashMutiplyer) + new Vector3(0, .8f, 0), ForceMode2D.Impulse);

                timer1 -= Time.deltaTime;
                if (timer1 <= 0)
                {
                    CanDash = false;
                    dashed = true;
                }
            }
            if (CanDash == false && rigidBody.velocity == Vector2.zero && dashed == true)
            {
                LookAround();
                dashed = false;
                timer3 = 2;
                if (!boxColider.IsTouching(playerCurcleColider))
                {
                    currentState = states.WALKTOPLAYER;
                }
            }
        }
        // cause a bounce to happen if colided with player.

    }
    // early stage attack script that was used for testing how to creat box colliders at will
    // as to cause dame to the player, similer method was created later for the players weapon.
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
    // this method just sets the enemy to move towars the players x axis.
    // while the player is in range the method chooses to make the dicision
    // to either dash or not to based on a modulus quary.
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
            choice = Random.Range(1, 10);
            if (choice % 3 == 1)
            {
                currentState = states.DASH;
                timer1 = .1f;
                CanDash = true;
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
    // this method make use of the invoke method from unity that allows a method to be called
    // after a time delay. this calls the flip sprite method.
    void LookAround()
    {
        StopMoving();
        questionMark.SetActive(true);

        Invoke("FlipSprite", .5f);
        Invoke("FlipSprite", 1f);
        Invoke("FlipSprite", 1.5f);
        Invoke("FlipSprite", 2f);

        currentState = states.IDLE;
    }

    // walk simply adds velocity to enemy in the direction it is faceing for a period of time
    // then it decids weather too keep walking or to change direction.
    public void Walk()
    {
        if (timer1 <= 0)
        {
            timer1 = Random.Range(2, 5);
            timer2 = .5f;
        }
        rigidBody.velocity = (new Vector3(Direction.x * speed, rigidBody.velocity.y, 0));
        timer1 -= Time.deltaTime;

        
        timer2 -= Time.deltaTime;
        if (timer2 <= 0 && Turned == true)
        {
            Turned = false;
            timer2 = .5f;
        }
        // add in a wall detection.
            //bounce off the wall just by flipping the sprite

        if (timer1 <= 0)
        {
            StopMoving();
            NextWalk();
        }

        if (boxColider.IsTouching(playerCurcleColider))
        {
            currentState = states.WALKTOPLAYER;
        }


        // enemyTurner is a single instance of an object present in the scene. the isuse is that it is only detecting one
        // object and not the two of them. need to create a list to contain all the enemyTurners in the scene
        // then foreach over them to detect tho collisions.
        //if (collider.IsTouching(EnemyTurner.GetComponent<BoxCollider2D>()) && Turned == false)
        //{
        //    FlipSprite();
        //    Turned = true;
        //}
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


