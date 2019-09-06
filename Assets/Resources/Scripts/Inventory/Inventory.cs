using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public enum ItemType { Block, Other }

public abstract class InventoryItem
{
    public ItemType itemType { get; protected set; }

    public abstract string itemName { get; protected set; }

    public abstract double baseCash { get; protected set; }

    private double _amount;
    public double amount
    {
        get { return _amount; }
        set
        {
            if (value <= 0)
                _amount = 0;
            else
                _amount = value;
        }
    }

    public abstract void UseItem();

    public double finalCash { get; private set; }

    public bool isEmpty { get; set; }

    public double SellItem(double amount, float multiplier)
    {
        if (this.amount > 0)
        {
            amount = ReduceAmount(amount);
            CalculateFinal();
            return finalCash * amount * multiplier;
        }
        return 0;
    }

    public double ReduceAmount(double amount)
    {
        if (this.amount < amount)
            amount = this.amount;

        this.amount -= amount;
        return amount;
    }

    public void CalculateFinal()
    {
        finalCash = baseCash;
    }
}

public class BlockItem : InventoryItem
{
    public int blockId { get; set; }
    public BlockInfo blockinfo { get; set; }
    public override string itemName { get; protected set; }
    public override double baseCash { get; protected set; }

    public BlockItem(int blockId, double amount)
    {
        blockinfo = Gamemanager.main.GetBlock(blockId);
        this.blockId = blockId;
        baseCash = blockinfo.cash;
        this.amount = amount;
        itemName = blockinfo.blockname.ToString();
        itemType = ItemType.Block;
    }

    public BlockInfo getBlockInfo()
    {
        return blockinfo;
    }

    public override void UseItem()
    {
        BuffManager.main.useBlockBuff(blockId);
    }
}

public class Inventory : MonoBehaviour {

    //Singleton
    public static Inventory main;

    public List<InventoryItem> inventoryList { get; private set; }

    public bool isRefreshed = false;

    [SerializeField]
    private UI_Inventory ui_inv;

    private void Awake()
    {
        inventoryList = new List<InventoryItem>();
        if (main == null)
            main = this;
        else
            Destroy(this);
    }

    void Start () {
        SaveScript.LoadInventory();
        RefreshInventory();
    }

    // Add item to the inventory. Increase it's amount if it already exists
    public InventoryItem AddInventory(string itemName, double amount, ItemType itemType)
    {
        // Fetch Item from the Inventory
        InventoryItem item = GetInventoryItem(itemName, itemType);

        // Check if that Item exist
        if (item == null)
        {
            switch (itemType)
            {
                case ItemType.Block:
                    item = new BlockItem(int.Parse(itemName), amount);
                    FetchToBlockInventoryUi(item as BlockItem);
                    break;
            }

            inventoryList.Add(item);
            
        }
        else
        {
            item.amount += amount;
        }

        RefreshInventory();

        return item;
    }

    public void SellItem(InventoryItem item, double amount)
    {
        double cash = item.SellItem(amount, 1);
        RefreshInventory();
        Gamemanager.main.player.GiveCash(cash);
    }

    public void UseItem(InventoryItem item, double amount)
    {
        item.amount -= amount;
        if(item.amount < 0)
        {
            item.amount = 0;
        }

        if (amount != 0) { 
            for (int i = 0; i<amount; i++) {
                item.UseItem();
            }
        }
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        for(int i=0; i< inventoryList.Count; i++)
        {
            inventoryList[i].isEmpty = inventoryList[i].amount <= 0 ? true : false;
            inventoryList[i].CalculateFinal();
        }
        isRefreshed = true;
    }

    public InventoryItem GetInventoryItem(string itemName, ItemType itemType)
    {
        switch(itemType)
        {
            case ItemType.Block:
                return getBlockItem(int.Parse(itemName));

            default:
                for (int i = 0; i < inventoryList.Count; i++)
                {
                    if (inventoryList[i].itemName.Equals(itemName))
                    {
                        return inventoryList[i];
                    }
                }
                return null;
        }
    }

    public BlockItem getBlockItem(int id)
    {
        BlockItem item = null;
        for (int i = 0; i<inventoryList.Count; i++)
        {
            item = inventoryList[i] as BlockItem;
            if(item != null)
            {
                if (item.blockId == id)
                    break;
                else
                    item = null;
            }
        }
        return item;
    }

    public void DeleteEntry(string itemName, ItemType itemType)
    {
        inventoryList.Remove(GetInventoryItem(itemName, itemType));
        RefreshInventory();
    }

    /*public bool IsItemAvailable(string name, double amount)
    {
        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (name == inventoryList[i].blockItem.blockname && amount <= inventoryList[i].amount)
            {
                return true;
            }
        }
        return false;
    } */

    void SortInventory()
    {
        //TODO implement Sorting
    }

    void FetchToBlockInventoryUi(BlockItem item)
    {
        GameObject obj = Instantiate(ui_inv.invItemPrefab) as GameObject;
        obj.transform.SetParent(ui_inv.invContentPanel.transform, false);
        obj.GetComponentInChildren<Text>().text = item.itemName.ToString();
        obj.GetComponent<Button>().onClick.AddListener(() => ui_inv.AssignCurrentItem(item.blockId));
        ui_inv.invButtons.Add(obj);
    }
}
