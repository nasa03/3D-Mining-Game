// This Script allows to regenerate random ores once you mine Dirt and certain other stuff//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blockmanager : MonoBehaviour {

	Block currentMinedBlock;
	GameObject[] spreadBlock = new GameObject[6];

	double mineLayer;

	// Use this for initialization
	void Start () {
	}

	public void BlockisMined(GameObject block) {
		SpreadBlocks (block.transform.position, block);
	}

	public void SpreadBlocks(Vector3 position, GameObject block)
	{
        /*if(blockCreator.levelEditor)
        {
            return;
        } */

        Gamemanager.main.refreshBlockList(block.transform.position.y);
        //TODO Autodetect Intersect before its been created
        spreadBlock [0] = Gamemanager.main.SpawnBlockByLayer(Vector3.left + position);
		spreadBlock [1] = Gamemanager.main.SpawnBlockByLayer(Vector3.up + position);
        spreadBlock [2] = Gamemanager.main.SpawnBlockByLayer(Vector3.forward + position);
		spreadBlock [3] = Gamemanager.main.SpawnBlockByLayer(Vector3.right + position);
		spreadBlock [4] = Gamemanager.main.SpawnBlockByLayer(Vector3.down + position);
		spreadBlock [5] = Gamemanager.main.SpawnBlockByLayer(Vector3.back + position);
	}
}
