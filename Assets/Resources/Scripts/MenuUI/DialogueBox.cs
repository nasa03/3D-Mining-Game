using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public GameObject dialogueGO;
    public GameObject boxPanel;
    public Button yesButton, noButton;
    public Text ui_text;
    bool wasPlayerControl = false;

    public void createDialogue(string text, UnityAction yesEvent, UnityAction noEvent)
    {
        dialogueGO.SetActive(true);

        if(Gamemanager.main.player.playerControl)
        {
            Gamemanager.main.player.playerControl = false;
            wasPlayerControl = true;
        }

        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        if(yesEvent != null)
            yesButton.onClick.AddListener(yesEvent);

        yesButton.onClick.AddListener(closeBox);

        if (noEvent != null)
            noButton.onClick.AddListener(noEvent);

        noButton.onClick.AddListener(closeBox);

        ui_text.text = text;

        boxPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 75 + ui_text.preferredHeight);
    }

    void closeBox()
    {
        if (wasPlayerControl)
        {
            Gamemanager.main.player.playerControl = true;
        }
        dialogueGO.SetActive(false);
    }
}
