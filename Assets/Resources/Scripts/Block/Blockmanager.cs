﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: Remove this script and make a prefab out of this in the future

public class Blockmanager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CreateRoom(new Vector3(0, -15, 0), true);
    }

    //TODO Block Prefab
    void CreateRoom(Vector3 positionToSpawn, bool isPlayerSpawn)
    {
        Gamemanager.main.RefreshBlockList((int)positionToSpawn.y);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(0.0f, 0.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-1.0f, 0.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-2.0f, 0.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-3.0f, 0.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(0.0f, 0.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-1.0f, 0.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-2.0f, 0.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-3.0f, 0.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-3.0f, 0.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-2.0f, 0.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-1.0f, 0.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(0.0f, 0.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(0.0f, 0.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-1.0f, 0.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-3.0f, 0.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-2.0f, 0.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-4.0f, 0.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-4.0f, 0.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-4.0f, 0.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-4.0f, 0.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-4.0f, 0.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-3.0f, 0.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-2.0f, 0.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(0.0f, 0.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-1.0f, 0.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(0.0f, 1.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(0.0f, 2.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(0.0f, 3.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-5.0f, 0.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-5.0f, 0.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-5.0f, 0.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-5.0f, 0.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-5.0f, 0.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-6.0f, 0.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-6.0f, 0.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-6.0f, 0.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-6.0f, 0.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-6.0f, 0.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(0.0f, 0.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-1.0f, 0.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-2.0f, 0.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-3.0f, 0.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-4.0f, 0.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-5.0f, 0.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-6.0f, 0.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-6.0f, 1.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-6.0f, 2.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-6.0f, 3.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(0.0f, 0.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-1.0f, 0.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-2.0f, 0.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-3.0f, 0.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-4.0f, 0.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-5.0f, 0.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-6.0f, 0.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-6.0f, 1.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-6.0f, 2.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-6.0f, 3.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(0.0f, 1.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(0.0f, 2.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(0.0f, 3.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-5.0f, 1.0f, -7.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-5.0f, 2.0f, -7.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-5.0f, 3.0f, -7.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-4.0f, 1.0f, -7.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-3.0f, 1.0f, -7.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-2.0f, 1.0f, -7.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-1.0f, 1.0f, -7.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-4.0f, 2.0f, -7.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-3.0f, 2.0f, -7.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-2.0f, 2.0f, -7.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-1.0f, 2.0f, -7.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-4.0f, 3.0f, -7.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-3.0f, 3.0f, -7.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-2.0f, 3.0f, -7.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-1.0f, 3.0f, -7.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-7.0f, 1.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-7.0f, 1.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-7.0f, 1.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-7.0f, 1.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-7.0f, 1.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-7.0f, 2.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-7.0f, 3.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-7.0f, 2.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-7.0f, 3.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-7.0f, 2.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-7.0f, 3.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-7.0f, 2.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-7.0f, 2.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-7.0f, 3.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-7.0f, 3.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-5.0f, 1.0f, 1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-5.0f, 2.0f, 1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-5.0f, 3.0f, 1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-4.0f, 1.0f, 1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-4.0f, 2.0f, 1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-4.0f, 3.0f, 1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-3.0f, 3.0f, 1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-3.0f, 2.0f, 1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-3.0f, 1.0f, 1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-2.0f, 1.0f, 1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-2.0f, 2.0f, 1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-2.0f, 3.0f, 1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-1.0f, 1.0f, 1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-1.0f, 2.0f, 1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(-1.0f, 3.0f, 1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(1.0f, 1.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(1.0f, 2.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(1.0f, 3.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(1.0f, 1.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(1.0f, 2.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(1.0f, 3.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(1.0f, 3.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(1.0f, 2.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(1.0f, 1.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(1.0f, 1.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(1.0f, 1.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(1.0f, 2.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(1.0f, 2.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(1.0f, 3.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlockByLayer(new Vector3(1.0f, 3.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-6.0f, 4.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-5.0f, 4.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-4.0f, 4.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-3.0f, 4.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-2.0f, 4.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-1.0f, 4.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(0.0f, 4.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(0.0f, 4.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-1.0f, 4.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-2.0f, 4.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-3.0f, 4.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-4.0f, 4.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-5.0f, 4.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-6.0f, 4.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-6.0f, 4.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-5.0f, 4.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-4.0f, 4.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-3.0f, 4.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-2.0f, 4.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(0.0f, 4.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-1.0f, 4.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(0.0f, 4.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-1.0f, 4.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-2.0f, 4.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-3.0f, 4.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-4.0f, 4.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-5.0f, 4.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-6.0f, 4.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(0.0f, 4.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-1.0f, 4.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-2.0f, 4.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-3.0f, 4.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-4.0f, 4.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-5.0f, 4.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-6.0f, 4.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-5.0f, 4.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-4.0f, 4.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-3.0f, 4.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-2.0f, 4.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-1.0f, 4.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-1.0f, 4.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-2.0f, 4.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-3.0f, 4.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-4.0f, 4.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(1, new Vector3(-5.0f, 4.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-1.0f, 1.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-1.0f, 2.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-1.0f, 3.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-2.0f, 3.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-2.0f, 2.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-2.0f, 1.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-3.0f, 1.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-3.0f, 2.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-3.0f, 3.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-4.0f, 3.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-4.0f, 1.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-4.0f, 2.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-5.0f, 1.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-5.0f, 2.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-5.0f, 3.0f, -6.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-6.0f, 3.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-6.0f, 2.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-6.0f, 1.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-6.0f, 1.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-6.0f, 2.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-6.0f, 3.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-6.0f, 3.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-6.0f, 2.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-6.0f, 1.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-6.0f, 1.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-6.0f, 2.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-6.0f, 3.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-6.0f, 3.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-6.0f, 2.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-6.0f, 1.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-5.0f, 1.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-5.0f, 2.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-5.0f, 3.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-4.0f, 3.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-4.0f, 2.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-4.0f, 1.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-3.0f, 1.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-3.0f, 2.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-3.0f, 3.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-2.0f, 3.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-2.0f, 2.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-2.0f, 1.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-1.0f, 1.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-1.0f, 2.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-1.0f, 3.0f, 0.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(0.0f, 3.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(0.0f, 2.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(0.0f, 1.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(0.0f, 1.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(0.0f, 2.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(0.0f, 3.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(0.0f, 3.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(0.0f, 2.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(0.0f, 1.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(0.0f, 1.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(0.0f, 2.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(0.0f, 3.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(0.0f, 3.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(0.0f, 2.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(0.0f, 1.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-5.0f, 1.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-5.0f, 1.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-5.0f, 1.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-5.0f, 1.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-5.0f, 1.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-4.0f, 1.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-3.0f, 1.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-1.0f, 1.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-2.0f, 1.0f, -1.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-1.0f, 1.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-1.0f, 1.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-1.0f, 1.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-1.0f, 1.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-2.0f, 1.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-3.0f, 1.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-4.0f, 1.0f, -5.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-4.0f, 1.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-4.0f, 1.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-4.0f, 1.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-3.0f, 1.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-2.0f, 1.0f, -2.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-2.0f, 1.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-2.0f, 1.0f, -4.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-3.0f, 1.0f, -3.0f) + positionToSpawn);
        Gamemanager.main.SpawnBlock(0, new Vector3(-3.0f, 1.0f, -4.0f) + positionToSpawn);


        if (isPlayerSpawn)
        {
            GameObject.Find("Player").transform.position = positionToSpawn + new Vector3(-3f, 0, -3);
        }
    }
}
