using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderMageRange : MonoBehaviour
{

    //Only used for debuging not needed, not even working

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Stay");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
    }
}
