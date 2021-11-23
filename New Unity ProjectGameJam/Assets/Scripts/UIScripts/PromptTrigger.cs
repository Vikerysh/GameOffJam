using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PromptTrigger : MonoBehaviour
{
    [Header("Variables")]
    bool textEnabled = false;
    //public GameObject prompt;


    [Header("Text to Display")]
    [TextArea(3, 10)]
    [SerializeField] string promptText;
    TMP_Text text;

    void Start()
    {
        text = GameController.instance.popUpText;
    }


    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player") 
        {
            text.text = promptText;
        }


    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player") 
        {
            
            text.text = " ";

        }

    }

}
