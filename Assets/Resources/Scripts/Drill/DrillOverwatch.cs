using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//W.I.P

public class DrillOverwatch : MonoBehaviour {

    public Text text, durabText;
    public bool hasDrillPurchased;
    public GameObject drill;
    public float update;
    float _update;

	// Use this for initialization
	void Start () {
        _update = update;
    }
	
	// Update is called once per frame
	void Update () {
        update -= Time.deltaTime;
        if(update <= 0)
        {
            if (drill == null)
            {
                text.text = "None";
            }
            if (hasDrillPurchased)
            {
                text.text = "Press V";
            }
            //durabText.text = player.drillDurability.ToString();
            update = _update;
        }
    }
}
