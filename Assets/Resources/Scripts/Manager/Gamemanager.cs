﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    //Accessable Classes
    public static Gamemanager main = null;
    public DialogueBox dialogue;

    //Game Flags
    public static bool[] GameFlags;

    // Tick
    private float tick = 1f;
    private float storeTick;
    public bool isTick;

    // Options
    public bool option_AutoClick;
    public bool option_AutoSell;
    public bool option_ShowTopUI;

    PlayerScript localPlayer;
    List<PlayerScript> joinedPlayers;
    public static int PlayerAmount;
    SaveScript SaveManager;


    // UI
    public GameObject hoverBox;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        if (main == null)
            main = this;
        else if (main != this)
            Destroy(gameObject);

        // Initiate Tick Variables
        storeTick = tick;
        isTick = false;

        SaveManager = gameObject.GetComponent<SaveScript>();

        //Perks.PerkSystem.InitPerks();
        SaveManager.ReceiveStoreData();


        joinedPlayers = new List<PlayerScript>();
        localPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        playerJoin(localPlayer);

    }

    //TODO: Save Flags
    void InitGameFlags()
    {

        /*   !!! WTF ARE Game Flags?! !!!
        *   
        *   Gameflags are booleans, they are used to identify certain rules, may it be tutorial stuff or part of a Perk.
        *   
        *   For example, if the flag in ID->0 is set to false, the tutorial seqment is going to be played. If the tutorial is done, ID->0 will be set to true.
        *   When the game is loaded again, the tutorial seqment will not be played.
        *  
        *   Check the Spreadsheet for the ID's
        *   
        *   W.I.P
        */

        GameFlags = new bool[1];
    }

    void Update()
    {
        TickUpdate();
    }

    // This Tick can be used on multiple scripts to manage updates in a least performance hitting scenario
    void TickUpdate()
    {
        tick -= Time.deltaTime;
        if (tick <= 0)
        {
            isTick = (tick <= 0 ? true : false);
            tick = storeTick;
        }
    }

    public void playerJoin(PlayerScript player)
    {
        joinedPlayers.Add(player);
        PlayerAmount++;
    }

    public void playerDisconnect(PlayerScript player)
    {
        joinedPlayers.Remove(player);
    }

    public PlayerScript getLocalPlayer()
    {
        return localPlayer;
    }

    public PlayerScript getPlayerByID(int id)
    {
        foreach(PlayerScript player in joinedPlayers)
        {
            if(player.playerId.Equals(id))
            {
                return player;
            }
        }
        return null;
    }

    public SaveScript getSaveScript()
    {
        return SaveManager;
    }

    /*public BlockInfo getBlock(string name)
    {
        foreach (BlockInfo block in availableBlocks)
        {
            if (block.blockname == name)
                return block;
        }
        return availableBlocks[0];
    } */

    public void Option_EnableAutoClick()
    {
        option_AutoClick = !option_AutoClick;
    }

    public void Option_EnableAutoSell()
    {
        option_AutoSell = !option_AutoSell;
    }

    public void Option_EnableShowTopUI()
    {
        option_ShowTopUI = !option_ShowTopUI;
        GameUITop.main.ToggleTopUI(option_ShowTopUI);
    }
}
