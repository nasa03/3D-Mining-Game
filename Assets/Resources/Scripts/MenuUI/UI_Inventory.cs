using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField]
    private Text ui_title, ui_desc, ui_collect, ui_sellButtonText;

    [SerializeField]
    private InputField ui_sellAmount;

    public GameObject invItemPrefab;

    public GameObject invContentPanel;

    public List<GameObject> invButtons = new List<GameObject>();

    private BlockItem currentItem;

    private BlockInfo currentBlockInfo;

    // Update is called once per frame
    void Update()
    {
        if (Inventory.main.isRefreshed)
        {
            RefreshInventoryUi();
            Inventory.main.isRefreshed = false;
        }
    }

    public void GetInventoryInfo()
    {
        currentBlockInfo = currentItem.blockinfo;

        ui_title.text = currentBlockInfo.blockname;
        ui_desc.text = currentBlockInfo.desc;
        ui_collect.text = "Owns: <b>" + currentItem.amount + "</b>\n\n" +
            "Value: <b>$" + currentBlockInfo.cash + "</b>\n" +
            "Total: <b>$" + currentItem.finalCash + "</b>";

        if (currentBlockInfo.isInteractable)
            ui_sellButtonText.text = "Use";
        else
            ui_sellButtonText.text = "Sell";

    }

    public void AssignCurrentItem(int blockId)
    {
        GameObject currentButton = EventSystem.current.currentSelectedGameObject;
        currentItem = (BlockItem)Inventory.main.GetInventoryItem(blockId.ToString(), ItemType.Block);
        ui_sellAmount.text = "1";
        GetInventoryInfo();
        RefreshInventoryUi();
    }

    void RefreshInventoryUi()
    {
        bool isHidden = false;
        int availableItems = 0;

        for (int i = 0; i < Inventory.main.inventoryList.Count; i++)
        {
            isHidden = HideButtonUi(invButtons[i], Inventory.main.inventoryList[i].isEmpty);
            
            if(!isHidden)
            {
                //Debug.Log("Block " + invSystem.inventoryList[i].item.blockname + " Exist!");
                invButtons[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(-6.5f, 0 - (20 * availableItems));
                //Debug.Log(availableItems);
                availableItems++;
            }
        }

        invContentPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 20 * availableItems);
    }

    bool DeleteButtonUi(GameObject button)
    {
        invButtons.Remove(button);
        Destroy(button);
        return true;
    }

    bool HideButtonUi(GameObject button, bool hideButton)
    {
        //invButtons.Remove(button);
        button.SetActive(!hideButton);
        return hideButton;
    }

    public void SellItem()
    {
        if (currentItem != null || currentItem.amount > 0)
        {
            if (currentBlockInfo.isInteractable)
            {
                Inventory.main.UseItem(currentItem, Mathf.Floor(Mathf.Abs(float.Parse(ui_sellAmount.text))));
            }
            else
                Inventory.main.SellItem(currentItem, Mathf.Floor(Mathf.Abs(float.Parse(ui_sellAmount.text))));
        }
        else
        {
            currentItem = null;
            return;
        }
        RefreshInventoryUi();
        GetInventoryInfo();
    }
}
