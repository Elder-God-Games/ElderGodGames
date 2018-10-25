using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    //Properties
    public GameObject player;
    public GameObject background;
    private float halfx;
    private float halfy;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        
            //FollowPlayer();

    }
    void FollowPlayer(){

        halfx = this.transform.position.x / 2;
        halfy = this.transform.position.y / 2;

        this.transform.position = new Vector3((player.transform.position.x / 2) - halfx, (player.transform.position.y / 2) - halfy);

    }
    void FollowBackground(){

        this.transform.position = new Vector3(background.transform.position.x - halfx, player.transform.position.y - halfy);
    }
}
