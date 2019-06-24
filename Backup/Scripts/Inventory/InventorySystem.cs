using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class InventoryItem
{
    private Blockscript _item;
    public Blockscript item
    {
        get { return _item; }
        set { _item = value; }
    }

    private float _amount;
    public float amount
    {
        get { return _amount; }
        set { _amount = value; }
    }

    private double _finalCash;
    public double finalCash
    {
        get { return _finalCash; }
        private set { _finalCash = value; }
    }

    private bool _isEmpty;
    public bool isEmpty
    {
        get { return _isEmpty; }
        set { _isEmpty = value; }
    }

    public InventoryItem(Blockscript item, float amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public InventoryItem(string item, float amount) : this(Gamemanager.main.getBlock(item), amount)
    {

    }

    public InventoryItem(Blockscript item) : this(item, 1)
    {

    }

    public InventoryItem(string item) : this(Gamemanager.main.getBlock(item), 1)
    {

    }

    public double sellItem(float amount, float multiplier)
    {
        if(this.amount <= 0)
        {
            return 0;
        }

        if (this.amount < amount)
        {
            amount = this.amount;
            this.amount = 0;
        }
        else
            this.amount -= amount;

        return (item.baseCash * amount) * multiplier;
    }

    public void calculateFinal() {
        finalCash = item.baseCash * amount;
    }
}

public class InventorySystem : MonoBehaviour {

    public List<InventoryItem> inventoryList = new List<InventoryItem>();
    public UI_Inventory ui_inv;
    public bool isRefreshed = false;
    
    // Use this for initialization
	void Start () {
        RefreshInventory();

        //Add to UI Inventory
    }

    public void AddInventory(InventoryItem item)
    {
        for (int i = 0; i<inventoryList.Count; i++)
        {
            if (inventoryList[i].item.name == item.item.name)
            {
                inventoryList[i].amount += item.amount;
                RefreshInventory();
                return;
            }
        }
        inventoryList.Add(item);
        fetchToInventoryUi(item.item.name);
        RefreshInventory();
    }

    public void AddInventoryFromBlock(Block blockName, float amount) {
       AddInventory(new InventoryItem(blockName.blockname, amount));
    }

    public void AddInventoryFromName(string blockName, float amount) {
        AddInventory(new InventoryItem(Gamemanager.main.getBlock(blockName).name, amount));
    }

    public double sellItem(InventoryItem item, float amount)
    {
        double cash = item.sellItem(amount, 1);
        RefreshInventory();
        Gamemanager.main.player.giveCash(cash);
        return cash;
    }

    public void RefreshInventory()
    {
        for(int i=0; i< inventoryList.Count; i++)
        {
            /*if(inventoryList[i].amount <= 0)
            {
                DeleteEntry(i);
                
            } */

            inventoryList[i].isEmpty = inventoryList[i].amount <= 0 ? true : false;
            inventoryList[i].calculateFinal();
        }
        isRefreshed = true;
    }

    public InventoryItem GetInventoryItem(string blockName)
    {
        for(int i = 0; i<inventoryList.Count; i++)
        {
            if(inventoryList[i].item.name == blockName)
            {
                return inventoryList[i];
            }
        }
        return null;
    }

    public void DeleteEntry(int _index)
    {
        inventoryList.RemoveAt(_index);
        RefreshInventory();
    }

    public void DeleteEntry(string blockName)
    {
        int i = 0;
        do
        {
            if(inventoryList[i].item.name == blockName)
            {
                inventoryList.RemoveAt(i);
                break;
            }
            i++;
        } while (i < inventoryList.Count);
        RefreshInventory();
    }

    public bool isItemAvailable(string name, float amount)
    {
        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (name == inventoryList[i].item.name && amount <= inventoryList[i].amount)
            {
                return true;
            }
        }
        return false;
    }

    void sortInventory()
    {
        //TODO implement Sorting
    }

    void fetchToInventoryUi(string itemName)
    {
        GameObject obj = Instantiate(ui_inv.invItemPrefab) as GameObject;
        obj.transform.SetParent(ui_inv.invContentPanel.transform, false);
        obj.GetComponentInChildren<Text>().text = itemName;
        obj.GetComponent<Button>().onClick.AddListener(() => ui_inv.assignCurrentItem());
        ui_inv.invButtons.Add(obj);
    }

    /*public void initInventoryUi()
    {
        for (int i = 0; i < inventoryList.Count; i++)
        {
            fetchToInventoryUi(inventoryList[i].item.name);
        }
    } */
}
