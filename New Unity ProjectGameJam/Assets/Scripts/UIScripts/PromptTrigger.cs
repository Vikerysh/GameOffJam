using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PromptTrigger : MonoBehaviour
{
    [Header("Variables")]
    bool playerisNear = false;
    bool textEnabled = false;
    public GameObject prompt;


    [Header("Text to Display")]
    [TextArea(3, 10)]
    [SerializeField] string promptText = ("Use the Arrow keys to move");

    [Header("References to Other Scripts")]
    PlayerMovement player; // in case we wanna trigger something in the player as he walks by this prompt



    void Start()
    {

    }

    void Update()
    {
        if (playerisNear)
        {
           
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player") 
        {
            playerisNear = true;

            if (!textEnabled)
            {
                TextPrompt popText = GameObject.FindGameObjectWithTag("PromptManager").GetComponent<TextPrompt>();
                popText.PopUpText(promptText);
            }
        }


    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player") 
        {
            TextPrompt popText = GameObject.FindGameObjectWithTag("PromptManager").GetComponent<TextPrompt>();
            popText.ClosePopUpText();
            playerisNear = false;

            prompt.SetActive(false);

        }

    }

}
