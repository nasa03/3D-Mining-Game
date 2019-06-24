using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField]
    private Text ui_title, ui_desc, ui_collect;

    [SerializeField]
    private InputField ui_sellAmount;

    public GameObject invItemPrefab;

    public GameObject invContentPanel;


    public List<GameObject> invButtons = new List<GameObject>();

    private InventorySystem invSystem;
    private InventoryItem currentItem;
    // Start is called before the first frame update
    void Start()
    {
        invSystem = Gamemanager.main.GetComponent<InventorySystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invSystem.isRefreshed)
        {
            refreshInventoryUi();
            invSystem.isRefreshed = false;
        }
    }

    public void getInventoryInfo()
    {
        ui_title.text = currentItem.item.name;
        ui_desc.text = currentItem.item.desc;
        ui_collect.text = "Owns: <b>" + currentItem.amount + "</b>\n\n" +
            "Value: <b>$" + currentItem.item.baseCash + "</b>\n" +
            "Total: <b>$" + currentItem.finalCash + "</b>";
    }

    public void assignCurrentItem()
    {
        GameObject currentButton = EventSystem.current.currentSelectedGameObject;
        currentItem = invSystem.GetInventoryItem(currentButton.GetComponent<Text>().text);
        ui_sellAmount.text = "1";
        getInventoryInfo();
        refreshInventoryUi();
    }

    void refreshInventoryUi()
    {
        bool isHidden = false;
        int availableItems = 0;

        for (int i = 0; i < invSystem.inventoryList.Count; i++)
        {
            isHidden = hideButtonUi(invButtons[i], invSystem.inventoryList[i].isEmpty);
            
            if(!isHidden)
            {
                Debug.Log("Block " + invSystem.inventoryList[i].item.name + " Exist!");
                invButtons[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(-6.5f, 0 - (20 * availableItems));
                Debug.Log(availableItems);
                availableItems++;
            }
        }

        invContentPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 20 * availableItems);
    }

    bool deleteButtonUi(GameObject button)
    {
        Debug.Log("Deleted Button " + button.GetComponent<Text>().text);
        invButtons.Remove(button);
        Destroy(button);
        return true;
    }

    bool hideButtonUi(GameObject button, bool hideButton)
    {
        Debug.Log("Hide Button " + button.GetComponent<Text>().text);
        //invButtons.Remove(button);
        button.SetActive(!hideButton);
        return hideButton;
    }

    public void sellItem()
    {
        if(currentItem != null || currentItem.amount > 0)
        {
            invSystem.sellItem(currentItem, Mathf.Floor(Mathf.Abs(float.Parse(ui_sellAmount.text))));
        }
        else
        {
            currentItem = null;
            return;
        }
        refreshInventoryUi();
        getInventoryInfo();
    }
}
