using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO:

public class Explode : MonoBehaviour {

	Collider[] explodeObject;
	public float speed;
	public bool ignoreOres;
    public int level;
    int levelStrenght = 3;
    int blocksMinedPerTick = 10;
    int currentLevel;
    int currentIndex;
    float storeSpeed;
	List<GameObject> arrayOfBlocks = new List<GameObject>();
    PlayerScript player;

	// Use this for initialization
	void Start () {
		storeSpeed = speed;
        currentLevel = 1;
        currentIndex = 0;
        player = Gamemanager.main.getLocalPlayer(); // Change this to when other Players trigger the bomb in the future
	}
	
	// Update is called once per frame
	void Update () {

		speed -= Time.deltaTime;
		if (speed <= 0)
		{
            if (arrayOfBlocks.Count == currentIndex)
            {
                FetchBlocks();
                currentIndex = 0;
            }
            else
            {
                for(int i = 0; i < blocksMinedPerTick; i++) { 
                    arrayOfBlocks[currentIndex].GetComponent<Block>().MineBlock(player);
                    currentIndex++;
                }
            }
            speed = storeSpeed;
        } 
	}

	void FetchBlocks()
	{
		explodeObject = Physics.OverlapSphere (transform.position, currentLevel * levelStrenght);
		arrayOfBlocks.Clear();

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
        
		if (currentLevel >= level) {
			Destroy(this.gameObject);
		}

        currentLevel++;
    }	
}
