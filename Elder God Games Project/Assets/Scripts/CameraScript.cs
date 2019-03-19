using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    //Properties
    public GameObject player;
    private float halfx;
    private float halfy;
    public float YOffsett;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    // simple script that sets the camera to follow the players position at
    // all tims with an offset that can be unut from the unity inspector.
    void Update() {
        
            FollowPlayer();

    }
    void FollowPlayer(){
        
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + YOffsett, this.transform.position.z);

    }
}
