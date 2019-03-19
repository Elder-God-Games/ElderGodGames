using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue: MonoBehaviour {

    // array to hold text
    public string[] CharacterDialogue;
    // reference to the text property on the canvas
    public Text Text;
    // reference to the canvas
    public Canvas Canvas;
    public bool PlayerInRange;

    private bool dialogueComplete = false;
    private int dialogueIndex = 0;

	// Use this for initialization
	void Start ()
    {
        // sets the Canvas.enabled to false by default
        SetCanvasVisibility(PlayerInRange);
        NextDialogue();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // if player is in range and there is still dialogue left
        if (PlayerInRange && !dialogueComplete)
        {
            // if the player presses the return key
            if (Input.GetKeyUp(KeyCode.Return))
            {
                // cycle the next line of dialogue
                NextDialogue();
            }
            // otherwise if the player inputs an action
            else if (Input.GetKeyUp(KeyCode.E) || Input.GetButtonUp("Jump"))
            {
                // skip the dialogue completely
                SkipDialogue();
            }
        }
        // if player is not in range or there is no dialogue left
        else
        {
            // set the Canvas.enabled to disabled
            SetCanvasVisibility(false);
        }
	}
    /// <summary>
    /// Cycle through the dialogue array
    /// </summary>
    void NextDialogue()
    {
        // if there is still dialogue left in the arry
        if (dialogueIndex < CharacterDialogue.Length)
        {
            // set the text property to the next index of the array
            Text.text = CharacterDialogue[dialogueIndex++];
        }
        else
        {
            SkipDialogue();
        }
    }
    /// <summary>
    /// Set the dialogueComplete property to true
    /// This will disable the Canvas the next time the Update is called
    /// </summary>
    void SkipDialogue()
    {
        dialogueComplete = true;
    }
    /// <summary>
    /// Set whether the Canvas isvisible or not
    /// </summary>
    /// <param name="visibility"></param>
    void SetCanvasVisibility(bool visibility)
    {
        Canvas.enabled = visibility;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the player collides with the trigger and dialogue is not yet complete 
        if (collision.gameObject.tag == "Player" && !dialogueComplete)
        {
            // set  PlayerInRange to true and toggle the canvas
            PlayerInRange = true;
            SetCanvasVisibility(PlayerInRange);
        }   
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // set  PlayerInRange to false and toggle the canvas
            PlayerInRange = false;
            SetCanvasVisibility(PlayerInRange);
        }
    }
}
