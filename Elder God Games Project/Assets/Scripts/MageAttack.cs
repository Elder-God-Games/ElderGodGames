using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using ColliderMageRange;

public class MageAttack : MonoBehaviour
{
    private Vector3 directionToTarget;

    //public CircleCollider2D playerCircleCollider;
    public CircleCollider2D PlayerColliderToHit;
    //public Rigidbody2D Rigidbody2D;
    public GameObject PlayableCharacter;
    public GameObject Fireball;
    //public GameObject Mage;
    //public GameObject FireballPrefab;

    //public CapsuleCollider2D CapsuleCollider;
    public BoxCollider2D boxCollider;
    public BoxCollider box;

    bool canAttack = true;

    public bool attackAbility = true;
    public float AttackDistance;

    //float distance = 5;
    public RaycastHit enemySightLine;
    Vector3 fwd;

    public float timer;

    private Rigidbody2D rigidBody;
    private Vector3 Direction = Vector3.left;
    SpriteRenderer spriteRenderer;
    FireballTargeter fireball;

    RaycastHit hit;
    float dist;

    // state machine
    enum states
    {
        IDLE,
        WAIT,
        ATTACK
    }
    states currentState;

    //public float dashDelayTime;

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
        if (PlayableCharacter != null)
        {
            directionToTarget = PlayableCharacter.transform.position - this.transform.position;
        }
        

        Debug.DrawRay(this.transform.position, directionToTarget, Color.red);

        switch (currentState)
        {
            case states.IDLE:
                Idle();
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
        canAttack = true;
        Physics.Raycast(transform.position, fwd, out enemySightLine, dist);

        #region Useless
        //if (enemySightLine.collider.tag == "Crate")
        //{
        //    Debug.DrawRay(transform.position, directionToTarget, Color.green);
        //    Debug.Log("Hello");
        //}

        //if (enemySightLine.collider.tag == "Crate")
        //{
        //    Debug.DrawRay(transform.position, directionToTarget, Color.green);
        //    Debug.Log("Hello");
        //}

        //if (enemySightLine.collider == box)
        //{
        //    Debug.DrawRay(transform.position, directionToTarget, Color.green);
        //    Debug.Log("Hello2");
        //}

        //if (Physics.Raycast(Mage.transform.position, fwd, out enemySightLine, dist))
        //{
        //    if (enemySightLine.transform.tag == "Crate")
        //    {
        //        Debug.Log("HI");
        //        Debug.DrawRay(Mage.transform.position, PlayableCharacter.transform.position, Color.red);
        //        currentState = states.IDLE;
        //    }
        //}

        //if (enemySightLine.collider == box)
        //{
        //    Debug.Log("HI");
        //    Debug.DrawRay(transform.position, directionToTarget, Color.red);
        //}
        //Debug.DrawRay(this.transform.position, directionToTarget, Color.red);
        #endregion
        if (Physics.Raycast(this.transform.position, directionToTarget, out hit))
        {
            if (hit.transform.tag == "Crate")
            {
                canAttack = false;
                Debug.Log("Crate");
            }
            else if (hit.transform.tag != null)
            {
                canAttack = true;
                Debug.Log("No Crate");
            }
        }
        #region Useless
        //if (Physics.Raycast(Mage.transform.position, PlayableCharacter.transform.position, out hit, dist))
        //{
        //    if (hit.transform.tag == "Platform")
        //    {
        //        canAttack = false;
        //    }
        //    else
        //    {
        //        canAttack = true;
        //    }
        //}
        #endregion
        if (canAttack == true)
        {
            Fireball.GetComponent<FireballTargeter>().enabled = true;
            Fireball.SetActive(true);
        }


        if (enemySightLine.collider != null)
        {
            Debug.DrawRay(this.transform.position, PlayableCharacter.transform.position, Color.red);
        }
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

    public void Idle()
    {
        float WaitTime = 1.5f;
        WaitTime -= Time.deltaTime;
        if (WaitTime < 0)
        {
            currentState = states.ATTACK;
        }
    }

    public void FlipSprite()
    {
        Direction = -Direction;
        spriteRenderer.flipX = !spriteRenderer.flipX;
        boxCollider.offset = new Vector2(-boxCollider.offset.x, boxCollider.offset.y);
    }

    void OnTriggerEnter2D(Collider2D col)//non-stop updating
    {
        if (col.tag == "Player")
        {
            Debug.Log("Enter" + col.gameObject.name + " : " + gameObject.name);
            //canAttack = true;
            currentState = states.ATTACK;
        }
    }

    void OnTriggerExit2D(Collider2D col)//non-stop updating
    {
        if (col.tag == "Player")
        {
            Debug.Log("Exit" + col.gameObject.name + " : " + gameObject.name);
            //canAttack = false;
            currentState = states.IDLE;
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
        //Gizmos.DrawLine(transform.position, PlayableCharacter.transform.position);
    }

}