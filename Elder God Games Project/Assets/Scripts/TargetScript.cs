using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour {

	private Vector3 MovingDirection = Vector3.left;    //initial movement direction

    private float min, max;
    public float travelDistance = 6;
    public float speed = 3;
    [Range(0, 10)]
    public float enemySpawn;
    public GameObject TowerEye, Player, Enemy;
    enum State { Idle, Attack}
    State Eye;
    public bool isDetected;

    // Use this for initialization
    void Start()
    {
        min = this.transform.position.x - (travelDistance / 2);
        max = this.transform.position.x + (travelDistance / 2);
        Eye = State.Idle;
        isDetected = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Eye = State.Attack;
            Debug.Log("Attack");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Eye == State.Idle)
        {
            UpdateMovement();
        }
        else if(Eye == State.Attack)
        {
            AttackStage();
        }
    }

    void UpdateMovement()
    {
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
        transform.position = Player.transform.position;
        StartCoroutine("waitSpawn");
    }

    IEnumerator waitSpawn()
    {
        yield return new WaitForSeconds(2.0f);
        Instantiate(Enemy, new Vector3(Player.transform.position.x, 5, 0), Quaternion.identity);
    }
}

