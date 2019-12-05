using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using LitJson;
using System.Linq;
using Perks;


public class SaveScript : MonoBehaviour
{ 
    JsonData savedData;
    public static bool isReset;
    string dataPath;

    private void Awake()
    {
        savedData = null;
        dataPath = Path.Combine(Application.persistentDataPath, "save.sav");
    }

    public void Save()
    {
        Debug.Log("Accessing Save");
        PlayerScript player = Gamemanager.main.getLocalPlayer();
        string jsonString = "{";

        using (StreamWriter streamWriter = File.CreateText(dataPath))
        {
            //Player Stats
            jsonString += "\"CurrencyCash\": " + "\"" + player.cash.ToString() + "\","
                + "\"PlayerDamage\": " + player.stats["damage"].level + ","
                + "\"PlayerSpeed\": " + player.stats["speed"].level + ","
                + "\"PlayerReach\": " + player.stats["reach"].level + ","
                + "\"PlayerCritChance\": " + player.stats["critical_chance"].level + ","
                + "\"PlayerCritDamage\": " + player.stats["critical_damage"].level + ","
                + "\"PlayerLuck\": " + player.stats["luck"].level + ","
                + "\"PlayerJet\": " + player.stats["jetpack_force"].level + ",";
            
            // Inventory
            jsonString += "\"Inventory\":[";

            for (int j = 0; j < Inventory.main.inventoryList.Count; j++)
            {
                if(Inventory.main.inventoryList[j] is BlockItem)
                {
                    BlockItem item = Inventory.main.inventoryList[j] as BlockItem;

                    jsonString += "{ \"Item\": " + item.blockId + ", \"Amount\": \"" + item.amount + "\", \"Type\": \"" + item.itemType + "\"";
                    if (j < Inventory.main.inventoryList.Count - 1)
                    {
                        jsonString += " },";
                    }
                    else
                    {
                        jsonString += " }";
                    }
                }                
            }
            jsonString += "],";

            // Perks
            jsonString += "\"Perks\":[";

            for (int j = 0; j < PerkSystem.activePerks.Count; j++)
            {
                jsonString += "{ \"ID\": \"" + PerkSystem.activePerks[j].id + "\", \"Level\": " + PerkSystem.activePerks[j].level;

                if (j < PerkSystem.activePerks.Count - 1)
                {
                    jsonString += "},";
                }
                else
                {
                    jsonString += "}";
                }
            }
            jsonString += "]";

            /*
            // Modifiers
            jsonString += "\"Stats\":[";

            for (int i = 0; i < player.stats.Count; i++)
            {
                string currentKey = player.stats.ElementAt(i).Key;

                jsonString += "{ \"Type\": \"" + currentKey + "\",";
                jsonString += "\"Modifier\":[ ";

                for (int j = 0; j<player.stats[currentKey].modifiers.Count; j++)
                {
                    jsonString += "{ \"Id\": \"" + player.stats[currentKey].modifiers[j].modifierId + "\",";
                    jsonString += "\"Stack\": " + player.stats[currentKey].modifiers[j].stack;

                    if (j < player.stats[currentKey].modifiers.Count - 1)
                    {
                        jsonString += " },";
                    }
                    else
                    {
                        jsonString += " }";
                    }
                }
                 
                if (i < player.stats.Count - 1)
                {
                    jsonString += " ]},";
                }
                else
                {
                    jsonString += " ]}";
                }
            }
            jsonString += "]"; */

            jsonString += "}";
            streamWriter.Write(jsonString);
        }
        Debug.Log("Save File Dumped into " + dataPath);
    }

    public void ReceiveStoreData()
    {
        if (isReset)
            return;        

        Debug.Log("Loading Game!");

        // Store the data in savedData
        try
        {
            string words = File.ReadAllText(dataPath);
            savedData = JsonMapper.ToObject(words);
        } catch (Exception e)
        {
            Debug.LogError("Unable to Load Data! " + e);
            isReset = true;
        }
    }
    
    public void LoadInventory()
    {
        if(savedData != null) { 
            for (int i = 0; i < savedData["Inventory"].Count; i++)
            {
                switch(savedData["Inventory"][i]["Type"].ToString())
                {
                    case "Block":
                        int blockId = (int)savedData["Inventory"][i]["Item"];
                        double blockAmount = double.Parse(savedData["Inventory"][i]["Amount"].ToString());
                        Inventory.main.AddInventory(blockId.ToString(), blockAmount, ItemType.Block);
                    break;

                }
            }
        }
    }

    public void LoadPerks()
    {
        if (savedData != null)
        {
            for (int i = 0; i < savedData["Perks"].Count; i++)
            {
                PerkSystem.AssignPerk(Gamemanager.main.getLocalPlayer(), savedData["Perks"][i]["ID"].ToString(), (int)savedData["Perks"][i]["Level"]);
            }
        }
    }

    public JsonData getSaveData()
    {
        return savedData;
    }

    void LoadModifiers()
    {
        //TODO
    }
}
