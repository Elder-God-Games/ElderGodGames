using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using ColliderMageRange;

public class MageAttack : MonoBehaviour
{
    private Vector3 directionToTarget;

    public CircleCollider2D playerCircleCollider;
    public CircleCollider2D PlayerColliderToHit;
    public Rigidbody2D Rigidbody2D;
    public GameObject PlayableCharacter;
    public GameObject Fireball;
    public GameObject FireballPrefab;

    //private Transform pTransform;

    public CapsuleCollider2D CapsuleCollider;
    public BoxCollider2D boxColider;

    bool canAttack = false;

    public bool attackAbility = true;
    public float AttackDistance;

    float distance = 5;
    public RaycastHit2D enemySightLine;

    public float timer;

    private Rigidbody2D rigidBody;
    private Vector3 Direction = Vector3.left;
    SpriteRenderer spriteRenderer;
    FireballTargeter fireball;

    // state machine
    enum states
    {
        IDLE,
        WAIT,
        ATTACK
    }
    states currentState;
    public float dashDelayTime;

    void Start()
    {
        currentState = states.IDLE;
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        fireball = GetComponent<FireballTargeter>();


        if (Fireball == null)
        {
            Fireball = GameObject.FindWithTag("Fireball");
        }

        //Instantiate(FireballPrefab, Fireball.transform.position, Fireball.transform.rotation);
    }

    void Update()
    {
        //Fireball.GetComponent<FireballTargeter>().enabled = true;
        directionToTarget = PlayableCharacter.transform.position - this.transform.position;
        

        switch (currentState)
        {

            case states.IDLE:
                currentState = states.ATTACK;
                break;

            case states.WAIT:
                Wait();
                break;

            case states.ATTACK:
                Attack();
                break;

            default:
                break;
        }

    }
    public void Attack()
    {
        enemySightLine = Physics2D.Raycast(transform.localPosition, PlayerColliderToHit.transform.position, 3);

        if (enemySightLine.collider != null)
        {
            Debug.DrawRay(transform.position, directionToTarget, Color.red);
        }

        if (canAttack)
        {
            Fireball.GetComponent<FireballTargeter>().enabled = true;
            Fireball.SetActive(true);
        }

        //if (!canAttack)
        //{
        //    currentState = states.WAIT;
        //}
    }

    public void Wait()
    {
        float WaitTime = 1.5f;
        WaitTime -= Time.deltaTime;
        if (WaitTime < 0)
        {
            currentState = states.IDLE;
        }
    }

    public void FlipSprite()
    {
        Direction = -Direction;
        spriteRenderer.flipX = !spriteRenderer.flipX;
        boxColider.offset = new Vector2(-boxColider.offset.x, boxColider.offset.y);
    }

    void OnTriggerEnter2D(Collider2D col)//non-stop updating
    {
        if (col.tag == "Player")
        {
            Debug.Log("Enter" + col.gameObject.name + " : " + gameObject.name);
            canAttack = true;
            Debug.Log(canAttack);
        }
    }

    void OnTriggerExit2D(Collider2D col)//non-stop updating
    {
        if (col.tag == "Player")
        {
            Debug.Log("Exit" + col.gameObject.name + " : " + gameObject.name);
            canAttack = false;
            Debug.Log(canAttack);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Stay" + col.gameObject.name + " : " + gameObject.name);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, PlayableCharacter.transform.position);
    }

}