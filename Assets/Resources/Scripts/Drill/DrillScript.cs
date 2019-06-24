using UnityEngine;
using System.Collections;

// W.I.P

public class DrillScript : MonoBehaviour {

    public float durability;
    public float speed;
    public int mineLevel;
    public float mineSpeed;
    public bool ignoreOres;
    public bool isMining;
    
	// Use this for initialization
	void Start () {
        transform.GetChild(0).GetComponent<TriggerDrillScript>().speed = mineSpeed;
        transform.GetChild(0).GetComponent<TriggerDrillScript>().mineLevel = mineLevel;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isMining)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if(durability <= 0)
        {
            Destroy(this.gameObject);
        }
	}

}
