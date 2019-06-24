using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SaveScript : MonoBehaviour
{

    InventorySystem inv;
    BlockCreating blockList;
    // Use this for initialization
    void Start()
    {
        inv = GetComponent<InventorySystem>();
        blockList = GetComponent<BlockCreating>();
    }

    public void Save()
    {
        Debug.Log("Accessing Save");

        foreach (Blockscript block in Gamemanager.main.getAllBlocks())
        {
            Debug.Log("Deleting Prefkey: " + PlayerPrefs.GetString("Ore" + block.name) + " And " + PlayerPrefs.GetString("OreAmount" + block.name));
            PlayerPrefs.DeleteKey("Ore" + block.name);
            PlayerPrefs.DeleteKey("OreAmount" + block.name);
            
        }
        for (int j = 0; j<inv.inventoryList.Count; j++)
        {
            PlayerPrefs.SetString("Ore" + inv.inventoryList[j].item.name, inv.inventoryList[j].item.name);
            PlayerPrefs.SetFloat("OreAmount" + inv.inventoryList[j].item.name, inv.inventoryList[j].amount);
            Debug.Log("Saving " + PlayerPrefs.GetString("Ore" + inv.inventoryList[j].item.name) + " with " + inv.inventoryList[j].amount);     
        }
        
        PlayerScript player = Gamemanager.main.player;
        //XPSystem xp = player.GetComponent<XPSystem>();
        PlayerPrefs.SetFloat("PlayerCash", player.cash);
        PlayerPrefs.SetInt("PlayerDamage",player.stats["damage"].level);
        PlayerPrefs.SetInt("PlayerSpeed", player.stats["speed"].level);
        PlayerPrefs.SetInt("PlayerReach", player.stats["reach"].level);
        PlayerPrefs.SetInt("PlayerCritChance", player.stats["critical chance"].level);
        PlayerPrefs.SetInt("PlayerCritDMG", player.stats["critical damage"].level);
        PlayerPrefs.SetInt("PlayerLuck", player.stats["luck"].level);
        PlayerPrefs.SetInt("PlayerJet", player.stats["jetpack force"].level);
        //PlayerPrefs.SetFloat("PlayerHeatSuit",player.heatsuit);
        //PlayerPrefs.SetFloat("PlayerRadSuit", player.radsuit);
        //PlayerPrefs.SetInt("PlayerDrillDur",player.drillDurability);
        //PlayerPrefs.SetInt("PlayerDrillLevel",player.drillLevel);
        /*
        PlayerPrefs.SetInt("XPLevel", xp.currentLevel);
        PlayerPrefs.SetInt("XPCurrent", xp.currentXP);
        PlayerPrefs.SetInt("XPNext", xp.nextXP);
        PlayerPrefs.SetFloat("XPMulti", xp.xpMultiplier); */

        ShopSystem shop = GameObject.Find("GameManager").GetComponent<ShopSystem>();
        for (int i=0; i<shop.createdItems.Count; i++)
        {
            if(!shop.createdItems[i].GetComponent<ShopItemScript>().noLimit)
            {
                PlayerPrefs.SetInt("Shop" + shop.createdItems[i].GetComponent<ShopItemScript>().itemName, shop.createdItems[i].GetComponent<ShopItemScript>().counter);
                Debug.Log(shop.createdItems[i].GetComponent<ShopItemScript>().itemName);
            }
        }
    }

    public void Load()
    {
        inv = GetComponent<InventorySystem>();
        foreach (Blockscript block in Gamemanager.main.getAllBlocks())
        {
            //Debug.Log("Loading " + PlayerPrefs.GetString("Ore" + block.name) + " with " + PlayerPrefs.GetFloat("OreAmount" + block.name));
            if (block.name == PlayerPrefs.GetString("Ore" + block.name) && block != null)
            {
                inv.AddInventoryFromName(block.name, PlayerPrefs.GetFloat("OreAmount" + block.name));
            }
        }
        ShopSystem shop = GameObject.Find("GameManager").GetComponent<ShopSystem>();
        for (int i = 0; i < shop.createdItems.Count; i++)
        {
            if (!shop.createdItems[i].GetComponent<ShopItemScript>().noLimit)
            {
                if(PlayerPrefs.HasKey("Shop" + shop.createdItems[i].GetComponent<ShopItemScript>().itemName))
                shop.createdItems[i].GetComponent<ShopItemScript>().counter = PlayerPrefs.GetInt("Shop" + shop.createdItems[i].GetComponent<ShopItemScript>().itemName);
            }
        }
    }
}
