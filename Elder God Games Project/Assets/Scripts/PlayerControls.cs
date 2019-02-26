using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject weapon;
    private float relativePos;
    
    // Methods
    void Start()
    {
        relativePos = 0.2f;
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Initiate.Fade("TitleScreen", Color.black, 1f);
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1"))
        {
            weapon.gameObject.SetActive(true);

            weapon.gameObject.transform.localPosition = this.gameObject.transform.right * relativePos;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If player enters contact with an enemy
        // do damage
    }
}
