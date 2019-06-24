using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUIScript : MonoBehaviour {

	public GameObject panel;
	public Text cashText, cordinate;
	FPSControl playerManager;
    BlockCreating blockManager;

	// Use this for initialization
	void Start () {
        playerManager = GameObject.Find ("Player").GetComponent<FPSControl> ();
        blockManager = GameObject.Find("GameManager").GetComponent<BlockCreating>();
	}
	
	// Update is called once per frame
	void Update () {
        int height = Mathf.FloorToInt(-playerManager.transform.position.y);
        
		cordinate.text = height.ToString() + Gamemanager.main.getLayerText(height);
		cashText.text = Gamemanager.main.player.cash.ToString ();
	
	}
}
