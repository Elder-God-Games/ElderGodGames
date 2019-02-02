using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    //Properties
    public GameObject player;
    private float halfx;
    private float halfy;
    private float yPos, zPos;
    [Range(1, 10)]
    public float lookAhead;


    // Use this for initialization
    void Start() {
        yPos = transform.position.y;
        zPos = transform.position.z;
    }

    // Update is called once per frame
    void Update() {
        FollowPlayer();
    }
    void FollowPlayer(){
        
        transform.position = new Vector3(player.transform.position.x + lookAhead, yPos, zPos);

    }
}
