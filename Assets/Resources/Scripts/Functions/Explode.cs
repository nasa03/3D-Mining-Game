using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO:

public class Explode : MonoBehaviour {

	Collider[] explodeObject;
	public float radius;
	public float speed;
	public bool ignoreOres;
	float storeSpeed;
	List<GameObject> arrayOfBlocks = new List<GameObject>();
    PlayerScript player;

	// Use this for initialization
	void Start () {
		storeSpeed = speed;
        player = Gamemanager.main.getLocalPlayer(); // Change this to when Player triggers the bomb in the future
	}
	
	// Update is called once per frame
	void Update () {

		speed -= Time.deltaTime;
		if (speed <= 0)
		{
			speed = storeSpeed;
			FetchBlocks();
			if(arrayOfBlocks.Count != 0)
			{
				arrayOfBlocks[0].GetComponent<Block>().MineBlock(player);
                arrayOfBlocks[1].GetComponent<Block>().MineBlock(player);
                arrayOfBlocks[2].GetComponent<Block>().MineBlock(player);
                arrayOfBlocks[3].GetComponent<Block>().MineBlock(player);
                arrayOfBlocks[4].GetComponent<Block>().MineBlock(player);
                arrayOfBlocks[5].GetComponent<Block>().MineBlock(player);
                arrayOfBlocks[6].GetComponent<Block>().MineBlock(player);
                arrayOfBlocks[7].GetComponent<Block>().MineBlock(player);
                arrayOfBlocks[8].GetComponent<Block>().MineBlock(player);
                arrayOfBlocks[9].GetComponent<Block>().MineBlock(player);
                arrayOfBlocks[10].GetComponent<Block>().MineBlock(player);
            }
		} 
	}

	void FetchBlocks()
	{
		explodeObject = Physics.OverlapSphere (transform.position, radius);
		arrayOfBlocks.Clear();

		if (arrayOfBlocks.Count == 0) {
			for (int i = 0; i<explodeObject.Length; i++) {
				if (explodeObject [i].tag == "Block" && explodeObject [i].tag != "Air" && !explodeObject[i].GetComponent<Block>().blockinfo.unBreakable) {
					if(ignoreOres && !explodeObject [i].GetComponent<Block> ().blockinfo.isOre)
					{
						arrayOfBlocks.Add (explodeObject [i].gameObject);
					}
					if(!ignoreOres)
					{
						arrayOfBlocks.Add (explodeObject [i].gameObject);
					}
				}
			}
		}
		if (arrayOfBlocks.Count == 0) {
			Destroy(this.gameObject);
		}
	}	
}
