using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class LevelEditorSave : MonoBehaviour {

	BlockCreating blockCreator;

	// Use this for initialization
	void Start () {
		blockCreator = GameObject.Find ("GameManager").GetComponent<BlockCreating> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P)) {
			Save();
		}
	
	}

	void Save()
	{
		if (blockCreator.levelEditor) 
		{
			GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

			StreamWriter stuff = File.CreateText(@"Test.txt");

			for(int i = 0; i<blocks.Length; i++)
			{
				//stuff.WriteLine("SpawnBlock(" + blocks[i].GetComponent<Block>().id + ", new Vector3(" + Mathf.FloorToInt(blocks[i].transform.position.x).ToString() + "f," + Mathf.FloorToInt(blocks[i].transform.position.y).ToString() + "f," + Mathf.FloorToInt(blocks[i].transform.position.z).ToString() + "f) + positionToSpawn);");
			}
			stuff.Close();
			Debug.Log("Saved!");
		}
	}
}
