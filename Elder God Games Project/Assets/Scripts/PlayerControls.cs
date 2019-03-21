using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject weapon;
    public PlayerMovement playerMovement;
    public SpriteRenderer spriteRenderer;
    public HealthBar health;
    // Position of the weapon relative to the Player Character
    public float relativePos;
    public float playerHealth = 1;
    AudioSource source;
    public AudioClip hit;

    bool isAttacking = false;
    
    // Methods
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
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
            weapon.GetComponent<SpriteRenderer>().flipX = true;
            
        }
        else if (playerMovement.horizontalMove > 0)  // face player right
        {
            spriteRenderer.flipX = false;

            // set weapon to the right hand side of the player
            weapon.gameObject.transform.localPosition = this.gameObject.transform.right * relativePos;
            weapon.GetComponent<SpriteRenderer>().flipX = false;
        }
        health.Health = playerHealth;
        if(playerHealth <= 0)
        {
            Initiate.Fade("FailScreen", Color.black, 2f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if(weapon.activeSelf == false)
            {
                Debug.Log("Touching Enemy");
                playerHealth -= 0.1f;
                if (!source.isPlaying)
                {
                    source.PlayOneShot(hit);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fireball")
        {
            if(weapon.activeSelf == false)
            {
                playerHealth -= 0.1f;
                if(!source.isPlaying)
                {
                    source.PlayOneShot(hit, 0.3f);
                }
            }
        }
    }
}
