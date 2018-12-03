using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    private Vector3 MovingDirection = Vector3.left;    //initial movement direction

    private float min, max;
    public float travelDistance = 6;
    public float speed = 3;

    // Use this for initialization
    void Start()
    {
        min = this.transform.position.x - (travelDistance / 2);
        max = this.transform.position.x + (travelDistance / 2);
    }

    void Update()
    {
        Update();
        
    }

    void UpdateMovement()
    {
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
}
