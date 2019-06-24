﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlockUiController : MonoBehaviour {

	FPSControl block;
	public Block currentBlock;
	public GameObject panel;
	public Text blockName, blockHealth, blockCash;
	public Slider healthSlider;
	Vector3 textPosition;
	public bool startShaking;
	float shaking;

	// Use this for initialization
	void Start () {
		textPosition = blockHealth.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Gamemanager.main.player.selectedBlock != null) {
			panel.SetActive(true);
			currentBlock = Gamemanager.main.player.selectedBlock.GetComponent<Block>();
			healthSlider.maxValue = (float)currentBlock.savedHealth;
			healthSlider.value = (float)currentBlock.health;
			blockName.text = currentBlock.blockname;
			blockName.color = new Color(currentBlock.color.r, currentBlock.color.g, currentBlock.color.b, 1f);
			blockHealth.text = Mathf.Floor((float)currentBlock.health).ToString();
			blockCash.text = currentBlock.baseCash.ToString();
			if(startShaking)
			{
				shaking = 5f;
				startShaking = false;
			}
			if(shaking > 0)
			{
				shaking -= Time.deltaTime * 8;
				blockHealth.transform.position = Vector3.Slerp(textPosition, new Vector3(textPosition.x + Random.Range(-shaking, shaking), textPosition.y + Random.Range(-shaking, shaking), textPosition.z), 1f);
			}

		}
		else
		{
			panel.SetActive(false);
		}
	
	}
}
