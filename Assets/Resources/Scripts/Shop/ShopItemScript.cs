using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ShopItemScript : MonoBehaviour {

    ShopSystem shopManager;
    public string itemName;
    public string itemDescription;
    public string itemID;
    public int reqLevel;
    public int buyLimit;
    public int buyTime;
    public int counter;
    public bool noLimit;
    public double money;
    public List<string> oreName = new List<string>();
    public List<int> oreAmount = new List<int>();
    static List<GameObject> tempHolder = new List<GameObject>();
    public enumCat category;
    Color originColor;
    GameObject hoverPanel;
    // Use this for initialization
    void Start () {
        shopManager = Gamemanager.main.GetComponent<ShopSystem>();
        GetComponent<Text>().text = itemName;
        originColor = this.GetComponent<Text>().color;
        checkIfLimit();
    }
	
	// Update is called once per frame
	void Update () {
        if (hoverPanel != null)
        {
            hoverPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y - hoverPanel.GetComponent<RectTransform>().sizeDelta.y / 2, 0);
        }
    }

    public void Hover(bool isHovering)
    {
        if (isHovering)
        {
            if (hoverPanel == null)
            {
                hoverPanel = Instantiate(Resources.Load("Prefab/HoverPanel")) as GameObject;
                hoverPanel.transform.SetParent(GameObject.FindGameObjectWithTag("MainCanvas").transform, false);
                float sizeY = 0;
                for (int i = -1; i < oreName.Count; i++)
                {
                    GameObject newText = Instantiate(hoverPanel.transform.Find("ReqText").gameObject) as GameObject;
                    newText.transform.SetParent(hoverPanel.transform, false);
                    newText.GetComponent<RectTransform>().anchoredPosition = new Vector2(10, -30 - (i + 1) * 12);
                    sizeY += 14;
                    if (i == -1)
                    {
                        newText.GetComponent<Text>().text = "$" + money;
                        if (money == 0)
                        {
                            newText.GetComponent<Text>().text = "$FREE";
                        }
                        if (Gamemanager.main.player.cash >= money)
                        {
                            newText.GetComponent<Text>().color = Color.green;
                        }
                        else
                        {
                            newText.GetComponent<Text>().color = Color.red;
                        }
                    }
                    else
                    {
                        newText.GetComponent<Text>().text = oreName[i] + " x" + oreAmount[i].ToString();
                        /*if (Inventory.main.IsItemAvailable(oreName[i], oreAmount[i]))
                        {
                            newText.GetComponent<Text>().color = Color.green;
                        }
                        else
                        {
                            newText.GetComponent<Text>().color = Color.red;
                        } */
                    }
                }
                hoverPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(hoverPanel.GetComponent<RectTransform>().sizeDelta.x, hoverPanel.GetComponent<RectTransform>().sizeDelta.y + sizeY);
            }
            
        }
        else
        {
            this.GetComponentInChildren<Text>().color = originColor;
            Destroy(hoverPanel);
        }
    }

    public void onClick()
    {
        checkIfLimit();
        shopManager.currentItem = this;
        Text shopName = GameObject.Find("ShopName").GetComponentInChildren<Text>();
        Text shopDesc = GameObject.Find("ShopDesc").GetComponentInChildren<Text>();
        Text counterText = GameObject.Find("CounterShop").GetComponentInChildren<Text>();
        Button buyButton = GameObject.Find("BuyButton").GetComponent<Button>();
        GameObject _reqPos = GameObject.Find("ReqPos");
        int[] tempStoreAmount = new int[oreAmount.Count];
        double tempCash = money * shopManager.buyXTimes;

        for(int i = 0; i<oreName.Count; i++)
        {
            tempStoreAmount[i] = oreAmount[i] * (shopManager.buyXTimes);
        }

        buyButton.onClick.RemoveAllListeners();
        if (noLimit)
        {
            shopName.text = itemName;
        }
        else
        {
            shopName.text = itemName + counter + "/" + buyLimit;
        }
        shopDesc.text = itemDescription;
        for(int i=0; i<tempHolder.Count; i++)
        {
            Destroy(tempHolder[i]);
        }
        tempHolder.Clear();
        int a = 0;

        for (int i = -1; i < oreName.Count; i++)
        {
            GameObject newText = Instantiate(_reqPos.transform.Find("ReqText").gameObject) as GameObject;
            newText.transform.SetParent(_reqPos.transform, false);
            newText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (-i - 1) * 14);
            if(i == -1)
            {
                newText.GetComponent<Text>().text = "$" + tempCash;
                if(money == 0)
                {
                    newText.GetComponent<Text>().text = "$FREE";
                }
                if(Gamemanager.main.player.cash >= tempCash)
                {
                    newText.GetComponent<Text>().color = Color.green;
                    a++;
                }
                else
                {
                    newText.GetComponent<Text>().color = Color.red;
                }
            }
            else
            {
                newText.GetComponent<Text>().text = oreName[i] + " x" + tempStoreAmount[i].ToString();
                /*if (Inventory.main.IsItemAvailable(oreName[i], tempStoreAmount[i]))
                {
                    newText.GetComponent<Text>().color = Color.green;
                    a++;
                }
                else
                {
                    newText.GetComponent<Text>().color = Color.red;
                } */
            }   

            if(a == oreName.Count + 1)
            {
                buyButton.onClick.AddListener(() => BuyItem(itemID));
                buyButton.interactable = true;
            }
            else
            {
                buyButton.interactable = false;
            }
            tempHolder.Add(newText);
        }
    }

    void BuyItem(string id)
    {
        for (int j = 1; j <= shopManager.buyXTimes; j++)
        {
            if(counter >= buyLimit && !noLimit)
            {
                break;
            }
            for (int i = 0; i < oreName.Count; i++)
            {
                //GameObject.Find("GameManager").GetComponent<InventorySystem>().UpdateInventory(oreName[i], -oreAmount[i]);
            }
            counter++;

            //Gamemanager.main.player.addCash(-money);
            GameObject.Find("GameManager").GetComponent<ShopSystem>().ExecuteID(id);
        }
        checkIfLimit();
        onClick();
    }

    void checkIfLimit()
    {
        if (counter >= buyLimit)
        {
            if (!noLimit)
            {
                gameObject.GetComponent<EventTrigger>().enabled = false;
                gameObject.GetComponent<Text>().color = Color.black;
            }
        }
    }
}
