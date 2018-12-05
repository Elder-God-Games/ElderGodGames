using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAttack : MonoBehaviour
{
    #region Neils RayExample
    //public Transform TransformToTrack;
    //public bool DrawLine;
    //public float TrackingDistance = 10;
    //float Distance = 100;
    //public bool UseTrackerDistance;
    //LineRenderer line;
    //RaycastHit result; 
    #endregion

    private Vector3 directionToTarget;

    public CircleCollider2D playerCircleCollider;
    public CircleCollider2D PlayerColliderToHit;
    public Rigidbody2D Rigidbody2D;
    public GameObject PlayableCharacter;

    private Transform pTransform;

    public CapsuleCollider2D CapsuleCollider;
    public BoxCollider2D boxColider;


    //Vector3 player = PlayerColliderToHit.transform.position;



    public bool attackAbility = true;
    public float AttackDistance;

    float distance = 5;
    public RaycastHit2D enemySightLine;

    public float timer;

    private Rigidbody2D rigidBody;
    private Vector3 Direction = Vector3.left;
    SpriteRenderer spriteRenderer;




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
        pTransform.position = PlayerColliderToHit.transform.position;
    }

    void Update()
    {

        directionToTarget = PlayableCharacter.transform.position - this.transform.position;

        switch (currentState)
        {

            case states.IDLE:
                #region set state to ATTACK
                currentState = states.ATTACK;
                #endregion

                break;

            case states.WAIT:

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
        #region Notes
        // creates a new box colider that is a trigger
        // Sprite fireball = new this.gameObject.AddComponent<BoxCollider2D>();
        // tests to see if the player is left or right of the enemy
        // sets the offset based on this. 
        #endregion

        enemySightLine = Physics2D.Raycast(transform.localPosition, PlayerColliderToHit.transform.position, 3); //Works
        //Physics2D.Raycast(transform.position, movement.Direction, SightDistance); //Neils

        #region Debugging Raycast (incorrect raycast)
        //if (Physics.Raycast(transform.localPosition, transform.localPosition)) //Works
        //{
        //    Debug.Log(PlayerColliderToHit.transform.position);
        //    Debug.DrawRay(transform.position, PlayerColliderToHit.transform.localPosition, Color.red, 3);
        //} 
        #endregion



        if (enemySightLine.collider != null)
        {
            Debug.Log(PlayerColliderToHit.transform.position);
            Debug.DrawRay(transform.position, directionToTarget, Color.red);
        }

        #region OldRayCast Example
        //if (Vector3.Distance(transform.position, PlayerColliderToHit.transform.position) < distance)
        //{
        //    if (Physics.Raycast(transform.position, (PlayerColliderToHit.transform.position - transform.position)))
        //    {
        //        if (enemySightLine.transform == PlayerColliderToHit.transform)
        //        {
        //            // In Range and i can see you!
        //            Debug.DrawRay(transform.position, PlayerColliderToHit.transform.position, Color.red);
        //        }
        //    }
        //} 
        #endregion

        #region Debug Boxcollider on side of mage
        //if (CapsuleCollider.IsTouching(PlayerColliderToHit))
        //{
        //    if (transform.position.x > playerCircleCollider.transform.position.x)
        //    {
        //        BoxCollider2D attackCollider = this.gameObject.AddComponent<BoxCollider2D>();
        //        attackCollider.isTrigger = true;
        //        attackCollider.offset = new Vector2(-0.16f, 0.05f);
        //    }
        //    else if (transform.position.x < playerCircleCollider.transform.position.x)
        //    {
        //        BoxCollider2D attackCollider = this.gameObject.AddComponent<BoxCollider2D>();
        //        attackCollider.isTrigger = true;
        //        attackCollider.offset = new Vector2(0.16f, 0.05f);
        //    }
        //}
        // turn off the attack colider or a memory leak exists.
        //attackAbility = !attackAbility;
        //currentState = states.WAIT; 
        #endregion

        #region Neils 3D Ray
        //transform.LookAt(TransformToTrack);

        //if (UseTrackerDistance && Vector3.Distance(transform.position, TransformToTrack.position) <= TrackingDistance)
        //{
        //    #region DrawLine
        //    if (DrawLine)
        //    {
        //        if (Physics.Raycast(transform.position, transform.forward, out result, Distance))
        //        {
        //            if (result.collider.tag != "Player")
        //            {
        //                line.SetPosition(1, result.point);
        //                line.enabled = false;
        //            }
        //            else
        //            {
        //                line.SetPosition(1, TransformToTrack.position);
        //                line.enabled = true;
        //            }
        //        }
        //        line.SetPosition(0, transform.position);
        //        line.SetPosition(1, TransformToTrack.position);
        //    }
        //    #endregion
        //}
        #endregion
    }

    public void FlipSprite()
    {
        //for flipping the player sprite and colider
        Direction = -Direction;
        spriteRenderer.flipX = !spriteRenderer.flipX;
        boxColider.offset = new Vector2(-boxColider.offset.x, boxColider.offset.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, PlayableCharacter.transform.position);
    }
}
#region ToDo
/*
Use a Ray as a sight of line check
BoxCollider for range check
spawn (Instantiate()) a cube above mage
throw at player
*/
#endregion