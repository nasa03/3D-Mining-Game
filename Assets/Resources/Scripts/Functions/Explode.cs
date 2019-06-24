using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Explode : MonoBehaviour {

	Collider[] explodeObject;
	public float radius;
	public float speed;
	public bool ignoreOres;
	float storeSpeed;
	List<GameObject> arrayOfBlocks = new List<GameObject>();

	// Use this for initialization
	void Start () {
		storeSpeed = speed;

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
				arrayOfBlocks[0].GetComponent<Block>().MineBlock();
                arrayOfBlocks[1].GetComponent<Block>().MineBlock();
                arrayOfBlocks[2].GetComponent<Block>().MineBlock();
                arrayOfBlocks[3].GetComponent<Block>().MineBlock();
                arrayOfBlocks[4].GetComponent<Block>().MineBlock();
                arrayOfBlocks[5].GetComponent<Block>().MineBlock();
                arrayOfBlocks[6].GetComponent<Block>().MineBlock();
                arrayOfBlocks[7].GetComponent<Block>().MineBlock();
                arrayOfBlocks[8].GetComponent<Block>().MineBlock();
                arrayOfBlocks[9].GetComponent<Block>().MineBlock();
                arrayOfBlocks[10].GetComponent<Block>().MineBlock();
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
