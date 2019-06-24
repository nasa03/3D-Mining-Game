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
				arrayOfBlocks[0].GetComponent<Block>().mineBlock();
			}
		} 
	}

	void FetchBlocks()
	{
		explodeObject = Physics.OverlapSphere (transform.position, radius);
		arrayOfBlocks.Clear();

		if (arrayOfBlocks.Count == 0) {
			for (int i = 0; i<explodeObject.Length; i++) {
				if (explodeObject [i].tag == "Block" && !explodeObject [i].GetComponent<Block> ().isReplaced && !explodeObject[i].GetComponent<Block>().unBreakable) {
					if(ignoreOres && !explodeObject [i].GetComponent<Block> ().isOre)
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
