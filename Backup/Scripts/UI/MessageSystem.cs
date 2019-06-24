using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageSystem : MonoBehaviour {
	
	public BlockCreating blockCreator;
	GameObject textPanel;
    GameObject messagePanel;


    // Use this for initialization
    void Start () {
		textPanel = Resources.Load ("Prefab/TextMessage") as GameObject;

		if (blockCreator.levelEditor) {
			PostAnnouncement ("You have enabled the Level Editor. This will allow you to create costum rooms and dungeons. Pressing P will save your progress in a .txt file with their vector and blocktype", 9f);
		} else {
			PostAnnouncement ("Hello there, welcome to my little game. Left click on a block to mine them. Right click to activate jetpack. Tab opens up a menu. Esc closes the game.", 7f);
		}
		}

	public string PostAnnouncement(string text, float duration)
	{
        if(messagePanel != null)
        {
            Destroy(messagePanel);
        }
        messagePanel = Instantiate(textPanel, textPanel.transform.position, Quaternion.identity) as GameObject;
		TextScript script = messagePanel.GetComponent<TextScript> ();
		script.setMessage (text, duration);
		return text;
	}

}
