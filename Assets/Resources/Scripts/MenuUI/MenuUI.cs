using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private GameObject menuTitle;
    [SerializeField]
    private GameObject hoverBox;
    [SerializeField]
    private GameObject scrollContent;

    [SerializeField]
    private GameObject buttonPanel, contentPanel;

    private List<GameObject> menuBottons;
    private List<GameObject> menuContents;

    private void Awake()
    {
        
    }

    void Start()
    {
        menuPanel.SetActive(false);
        InitButtons();
    }

    void Update()
    {

        //MenuPanel.SetActive(!PlayerManager.player.playerControl);
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            menuPanel.SetActive(!menuPanel.activeSelf);
            Gamemanager.main.GetComponent<SaveScript>().Save();
            Gamemanager.main.player.playerControl = !Gamemanager.main.player.playerControl;
            hoverBox.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !Gamemanager.main.player.playerControl)
        {
            menuPanel.SetActive(false);
            Gamemanager.main.GetComponent<SaveScript>().Save();
            Gamemanager.main.player.playerControl = true;
            hoverBox.SetActive(false);
        }

    }

    // Initializes the buttons
    private void InitButtons()
    {
        menuBottons = new List<GameObject>();
        menuContents = new List<GameObject>();

        AddNavigationOnButton("Upgrade");
        AddNavigationOnButton("Inventory");
        AddNavigationOnButton("Shop");
        AddNavigationOnButton("Perks");
        AddNavigationOnButton("Dimensions");
        AddNavigationOnButton("Stats");
        AddNavigationOnButton("Options");
    }

    // Assign the navigation and other Events to the button
    private void AddNavigationOnButton(string buttonName)
    {
        GameObject currentButton = buttonPanel.transform.Find("Button_" + buttonName).gameObject;
        GameObject currentContent = contentPanel.transform.Find("Content_" + buttonName).gameObject;

        //Check if button has the component "Button"
        if (!currentButton.GetComponent<Button>())
        {
            currentButton.AddComponent<Button>();
        }
        currentButton.GetComponent<Button>().onClick.AddListener(() => NavigateOnClick(currentContent));

        //Check if button has the component "EventTrigger"
        if (!currentButton.GetComponent<EventTrigger>())
        {
            currentButton.AddComponent<EventTrigger>();
        }
        currentButton.GetComponent<EventTrigger>().AddListener(EventTriggerType.PointerEnter, (EventHandle) => OnButtonHover(currentButton));

        menuBottons.Add(currentButton);
        menuContents.Add(currentContent);

        Debug.Log(currentButton + " " + currentContent);
    }

    public void NavigateOnClick(GameObject content) {
        for (int i = 0; i< menuContents.Count; i++) {
            menuContents[i].gameObject.SetActive(false);
        } 
        //
        scrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -(350 - content.GetComponent<RectTransform>().sizeDelta.y));
        menuTitle.GetComponentInChildren<Text>().text = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        content.SetActive(true);
        AudioManager.main.Play("UI_Click", Random.Range(0.75f, 1.5f), gameObject, true);
    }

    public void OnButtonHover(GameObject go)
    {
        AudioManager.main.Play("UI_Hover", Random.Range(0.75f, 1.25f), go, true);
    }
    /*
    void updatePanel()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i].GetComponent<panelScript>().isClosed)
            {
                panels[i].SetActive(false);
                continue;
            }
            if (panels[i].GetComponent<panelScript>().isSticked)
            {
                panels[i].SetActive(true);
                continue;
            }
            panels[i].SetActive(!Gamemanager.main.player.playerControl);
        }
    } */
}
