using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUIScript : MonoBehaviour {

	public GameObject panel;
	public Text cashText, cordinate;
	FPSControl playerManager;

	// Use this for initialization
	void Start () {
        playerManager = GameObject.Find ("Player").GetComponent<FPSControl> ();
	}
	
	// Update is called once per frame
	void Update () {
        int height = Mathf.FloorToInt(-playerManager.transform.position.y);
        
		cordinate.text = height.ToString();
		cashText.text = GameFormat.toScientificNotation(Gamemanager.main.player.cash);
	
	}
}
