using UnityEngine;
using System.Collections;

public class InvPanelScript : MonoBehaviour {

    bool wasOpened = false;
    // Use this for initialization

    public void ShowTutorial()
    {
        if(!wasOpened)
        {
            wasOpened = true;
            GameObject.Find("GameManager").GetComponent<MessageSystem>().PostAnnouncement("This is your inventory. it will keep track of all the ores you mined. Right click on an ore to open up options.", 5f);
        }
    }
}
