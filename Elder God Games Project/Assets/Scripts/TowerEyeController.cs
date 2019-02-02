using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEyeController : MonoBehaviour {

    public GameObject player;
    public GameObject target;

    Collision collision;

    private RaycastHit hit;
    
    public bool isDetected = false;
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        if(isDetected == false)
            transform.LookAt(target.transform);
        else if(isDetected == true)
            transform.LookAt(player.transform);

        //Vector3 raycastDir = target.transform.position - transform.position;
        //Physics.Raycast(transform.position, raycastDir);
    }

    


}
