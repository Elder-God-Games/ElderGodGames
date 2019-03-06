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
            
            case states.IDLE:
                #region
                //add proper idle timer.

                currentState = states.WALK;
                timer1 = Random.Range(3, 4);
                timer2 = 2;
                
                #endregion
                break;
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

    void LookAround()
    {
        StopMoving();
        questionMark.SetActive(true);

        Invoke("FlipSprite", .2f);
        Invoke("FlipSprite", .8f);
        Invoke("FlipSprite", 1.8f);
        Invoke("FlipSprite", 2.8f);

        currentState = states.IDLE;
    }


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


