using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum enumCat { Upgrade, Buffs, Drill, Bomb, Misc }
[System.Serializable]
public class ShopClass
{
    public string name;
    public string description;
    public string id;
    public enumCat category;
    public List<ItemRequierment> oreReq;
    public double money;
}
[System.Serializable]
public struct ItemRequierment
{
    public string blockname;
    public int amount;
}

public class ShopSystem : MonoBehaviour {

    public List<ShopClass> items;
    public List<GameObject> createdItems = new List<GameObject>();
    public GameObject contentPanel;
    public int buyXTimes = 1;
    float refreshTimer = 7200; // 7200 seconds -> 2 Hours
    public ShopItemScript currentItem;

	// Use this for initialization
	void Start () {
        CreateItem();
        buyXTimes = 1;
	}

    public void CreateItem()
    {
        for (int i = 0; i < items.Count; i++)
        {
            GameObject obj = Instantiate(Resources.Load("Prefab/ShopItem")) as GameObject;
            obj.transform.SetParent(contentPanel.transform, false);
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 15 * -i);
            obj.GetComponent<ShopItemScript>().itemName = items[i].name;
            obj.GetComponent<ShopItemScript>().itemDescription = items[i].description;
            obj.GetComponent<ShopItemScript>().money = items[i].money;
            obj.GetComponent<ShopItemScript>().itemID = items[i].id;
            if(obj.GetComponent<ShopItemScript>().buyLimit == 0)
            {
                obj.GetComponent<ShopItemScript>().noLimit = true;
            }
            for (int j=0; j<items[i].oreReq.Count; j++)
            {
                obj.GetComponent<ShopItemScript>().oreName.Add(items[i].oreReq[j].blockname);
                obj.GetComponent<ShopItemScript>().oreAmount.Add(items[i].oreReq[j].amount);
            }
            createdItems.Add(obj);
        }
    }

    public void ExecuteID(string id)
    {
        if(id == "S_dirtyJob")
        {
            //Gamemanager.main.player.addCash(5);
        }
        if(id == "S_pickup1")
        {
            //Gamemanager.main.player.addStrength(1);
        }
        if(id == "S_ftoe")
        {
            //GameObject.Find("GameManager").GetComponent<InventorySystem>().UpdateInventory("Crate-F", -5);
            GameObject.Find("GameManager").GetComponent<InventorySystem>().AddInventoryFromName("Crate-E", 1);
        }
        /*if(id == "S_heatsuit")
        {
            Gamemanager.main.player.addHeatsuit(1);
        }
        if(id == "S_radsuit")
        {
            Gamemanager.main.player.addRadsuit(1);
        } */
        if(id == "S_drill1")
        {
            //Gamemanager.main.player.addDrillDurability(250 * Gamemanager.main.player.drillLevel);
            GameObject.FindObjectOfType<DrillOverwatch>().hasDrillPurchased = true;
        }
        if(id == "S_XPup")
        {
            //GameObject.Find("Player").GetComponent<XPSystem>().xpMultiplier += 0.5f;
        }
    }

    public void changeCount(int value)
    {
        buyXTimes += value;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            if(value <= 0)
            {
                buyXTimes = 1;
            }
            if(value >= 0)
            {
                buyXTimes = 256;
            }
        }

        if(Input.GetKey(KeyCode.LeftControl))
        {
            if (value <= 0)
            {
                buyXTimes -= 10;
            }
            if (value >= 0)
            {
                buyXTimes += 10;
            }
        }

        if (buyXTimes <= 0)
        {
            buyXTimes = 1;
        }
        GameObject.Find("CounterShop").GetComponent<Text>().text = buyXTimes.ToString();
        currentItem.onClick();
        
    }
    
}
