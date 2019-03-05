using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    //Properties
    public GameObject player;
    private float halfx;
    private float halfy;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        
            FollowPlayer();

    }
    void FollowPlayer(){
        
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);

    }
}
