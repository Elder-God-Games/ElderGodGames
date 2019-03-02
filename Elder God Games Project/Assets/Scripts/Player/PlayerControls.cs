using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject weapon;
    public PlayerMovement playerMovement;
    public SpriteRenderer spriteRenderer;
    // Position of the weapon relative to the Player Character
    public float relativePos;
    
    // Methods
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        // if 'Escape' on the Keyboard is pressed
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            // fade out to titlescreen
            Initiate.Fade("TitleScreen", Color.black, 1f);
        }

        // if the 'Right Mouse Button', the 'E' key or the 'Fire button' is pressed
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1"))
        {
            // set weapon to active
            weapon.gameObject.SetActive(true);

        }

        if (playerMovement.horizontalMove < 0) // face player left
        {
            spriteRenderer.flipX = true;
            
            // set weapon to the left hand side of the player
            weapon.gameObject.transform.localPosition = -this.gameObject.transform.right * relativePos;
        }
        else if (playerMovement.horizontalMove > 0) // face player right
        {
            spriteRenderer.flipX = false;

            // set weapon to the right hand side of the player
            weapon.gameObject.transform.localPosition = this.gameObject.transform.right * relativePos;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If player enters contact with an enemy
        // do damage
    }
}
