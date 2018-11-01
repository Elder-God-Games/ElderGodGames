using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAndMoveTowards : MonoBehaviour {

    public CircleCollider2D player;
    public CapsuleCollider2D colider;
    public bool attackAbility;
    float distance;
    public float AttackDistance;
    public float DashDistance;
    public float speed;
    public Sprite target;
    private int count = 1;
    enum states
    {
        IDLE,
        WALK,
        ATTACK,
        DASH
    }
    states currentState;
    // Use this for initialization
    void Start()
    {
        currentState = states.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(colider.transform.position, player.transform.position);

        

            switch (currentState)
        {
            case states.IDLE:
                if (colider.IsTouching(player) && currentState != states.ATTACK)
                {
                    currentState = states.WALK;
                }
                break;
            case states.WALK:

                // gets the enemy to move towards the players x position
                    transform.position = Vector2.MoveTowards(
                    transform.position,
                    new Vector2(player.transform.position.x, transform.position.y),
                    speed * Time.deltaTime);

                // finds if the player is inside the colider but also within a certain distance of the player
                if (distance < AttackDistance && colider.IsTouching(player))
                {
                    currentState = states.ATTACK;
                }
                //only works on one side.
                else if (distance >= DashDistance && distance > AttackDistance)
                {
                    currentState = states.DASH;
                }
                else if (!colider.IsTouching(player))
                {
                    currentState = states.IDLE;
                }
                break;
            case states.ATTACK:
                //section for turning on and off attacks for testing later.
                if (attackAbility)
                {
                    // creates a new box colider that is a trigger
                    BoxCollider2D attackCollider = this.gameObject.AddComponent<BoxCollider2D>();
                    attackCollider.isTrigger = true;
                    // tests o see if the player is left or right of the enemy
                    // sets the offset based on this.
                    if (transform.position.x > player.transform.position.x)
                    {
                        attackCollider.offset = new Vector2(-0.16f,0.05f);
                    }
                    else if (transform.position.x < player.transform.position.x)
                    {
                        attackCollider.offset = new Vector2(0.16f,0.05f);
                    }
                    // turn off the attack colider or a memory leak exists.
                    attackAbility = !attackAbility;
                }
                else
                {

                }
                // sets the enemy back to walk or idele
                if (colider.IsTouching(player) && distance > AttackDistance)
                {
                    currentState = states.WALK;
                }
                break;
            case states.DASH:
                // the dash keeps moving
                // stopPoint is constantly moving
                //Vector3 stopPoint = transform.position - new Vector3(5, 0, 0);
                //transform.position = Vector2.MoveTowards(transform.position, stopPoint, (speed * 3) * Time.deltaTime);
                //Debug.Log(stopPoint);
                
                if (count < 1)
                {
                    GameObject go1 = new GameObject("Target1");
                    GameObject go2 = new GameObject("Target2");
                    SpriteRenderer renderer1 = go1.AddComponent<SpriteRenderer>();
                    SpriteRenderer renderer2 = go2.AddComponent<SpriteRenderer>();
                    renderer1.sprite = target;
                    renderer2.sprite = target;
                    // change the negative to increse or decreses distace
                    go1.transform.position = new Vector3(transform.position.x - 2, 0.5f);
                    go2.transform.position = new Vector3(transform.position.x + 2, 0.5f);
                    go1.transform.localScale = new Vector3(0.16f, 0.16f, 0);
                    go2.transform.localScale = new Vector3(0.16f, 0.16f, 0);
                    count++;
                }
                currentState = states.IDLE;
                break;

            default:
                break;
        }

        
    }

    void DebugDetails()
    {
        Debug.Log("Player Position = " + player.transform.position);
        Debug.Log("Player Position = " + colider.transform.position);
        Debug.Log("Dash Positions = " + (colider.transform.position - new Vector3(5, 0, 0)));
        Debug.Log("Dash Positions = " + (colider.transform.position + new Vector3(5, 0, 0)));
    }
}


