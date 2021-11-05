using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TextPrompt : MonoBehaviour
{
    [TextArea(3, 10)]
    public string writtenText;

    public GameObject popUpBox; // Gameobject used to insert dialogue into
    public TMP_Text popUpText;


    public void PopUpText(string text)
    {
        popUpBox.SetActive(true);
        popUpText.text = text;
    }

    public void ClosePopUpText()
    {
        popUpBox.SetActive(false);
    }
}
