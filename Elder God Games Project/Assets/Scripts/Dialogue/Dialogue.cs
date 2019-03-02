﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue: MonoBehaviour {

    public string[] CharacterDialogue;
    public Text Text;
    public Canvas Canvas;
    public bool PlayerInRange;

    private bool dialogueComplete = false;
    private int dialogueIndex;

	// Use this for initialization
	void Start ()
    {
        Canvas.enabled = PlayerInRange;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (PlayerInRange && !dialogueComplete)
        {
            if (Input.GetKey(KeyCode.E) || Input.GetButton("Jump"))
            {
                NextDialogue();
            }
        }
        else
        {
            SetCanvasVisibility(false);
        }
	}
    void NextDialogue()
    {
        if (dialogueIndex < CharacterDialogue.Length)
        {
            Text.text = CharacterDialogue[dialogueIndex];
        }
        else
        {
            dialogueComplete = true;
        }
    }
    void SetCanvasVisibility(bool visibility)
    {
        Canvas.enabled = visibility;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !dialogueComplete)
        {
            PlayerInRange = true;
            SetCanvasVisibility(PlayerInRange);
        }   
    }
}
