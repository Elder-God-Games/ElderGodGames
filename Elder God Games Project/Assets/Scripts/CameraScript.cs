using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target, dialoguePanel;
    public Vector3 offset;
    Vector3 targetPos;
    private float yPos;
    // Use this for initialization
    void Start()
    {
        targetPos = transform.position;
        yPos = 1.1f;
    }

    void Update()
    {
        if(dialoguePanel.activeSelf == false)
        {
            offset = new Vector3(offset.x, yPos, offset.z);
        }
        else if(dialoguePanel.activeSelf == true)
        {
            offset = new Vector3(offset.x, 0.5f, offset.z);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;

            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * 15f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);

        }
    }
}
