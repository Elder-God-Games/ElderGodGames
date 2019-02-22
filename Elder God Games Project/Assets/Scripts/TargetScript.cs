using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TargetScript : MonoBehaviour
{

    private Vector3 MovingDirection = Vector3.left;    //initial movement direction

    private float min, max;
    public float travelDistance = 6;

    [Range(1, 5)]
    public float speed;
    [Range(0,5)]
    public float followingSpeed;
    [Range(0, 10)]
    public float enemySpawn;

    public GameObject TowerEye, Player, Enemy;

    enum State { Idle, Attack, Searching}
    State EyeState;
    
    int EnemyCount = 0;

    private float towerEyeYpos;


    // Use this for initialization
    void Start()
    {
        min = this.transform.position.x - (travelDistance / 2);
        max = this.transform.position.x + (travelDistance / 2);

        towerEyeYpos = transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D col)
    { 
        if(col.gameObject == Player)
            EyeState = State.Attack;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EyeState = State.Searching;
    }

    // Update is called once per frame
    void Update()
    {
        //Raycast Detection
        RaycastHit hit;

        Vector3 fwd = TowerEye.transform.TransformDirection(Vector3.forward); //raycast directing in eye direction
        float dist = Vector3.Distance(TowerEye.transform.position, Player.transform.position); //distance from eye to player

        Debug.DrawRay(TowerEye.transform.position, fwd * dist, Color.green);

        if(Physics.Raycast(TowerEye.transform.position, fwd, out hit, 50))
        {
            //Debug.Log(hit.collider.gameObject.tag);

            //If player is behind crate
            if (hit.transform.tag == "Crate")
            {
                //Debug.Log("Hit!");
                EyeState = State.Idle;
            }
        }
        switch(EyeState)
        {
            case State.Idle:
                UpdateMovement();
                break;

            case State.Attack:
                AttackStage();
                break;

            case State.Searching:
                SearchStage();
                break;

        }
        if (EyeState == State.Idle)
        {
            UpdateMovement();
        }
        else if (EyeState == State.Attack)
        {
            AttackStage();
        }
    }

    void SearchStage()
    {
        float step = 3 * Time.deltaTime;
        
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), step);

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x - 2, transform.position.y, transform.position.z), step);

        EyeState = State.Idle;
    }

    void UpdateMovement()
    {
        float step = speed * Time.deltaTime;

        if (transform.position.y > towerEyeYpos)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, towerEyeYpos, transform.position.z), step);

        TowerEye.transform.LookAt(transform);
        if (this.transform.position.x > max)
        {
            MovingDirection = Vector3.left * speed;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

        }
        else if (this.transform.position.x < min)
        {
            MovingDirection = Vector3.right * speed;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

        }
        this.transform.Translate(MovingDirection * Time.smoothDeltaTime);
    }


    void AttackStage()
    {
        TowerEye.transform.LookAt(Player.transform);

        //transform.position = Player.transform.position;

        float step = followingSpeed * Time.deltaTime;
        if(transform.position.y >= towerEyeYpos)
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, step);
        else if(transform.position.y < towerEyeYpos)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player.transform.position.x, towerEyeYpos, Player.transform.position.z), step);

        //while(EnemyCount != 3)
        //{
        //    StartCoroutine("waitSpawn");
        //}
    }

    IEnumerator waitSpawn()
    {
        yield return new WaitForSeconds(2.0f);
        Instantiate(Enemy, new Vector3(Player.transform.position.x, 5, 0), Quaternion.identity);
        EnemyCount++;
    }

}
