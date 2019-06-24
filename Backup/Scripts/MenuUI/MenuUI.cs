using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject[] menuContents;
    public GameObject menuTitle;
    public GameObject hoverBox;
    // Use this for initialization
    void Start()
    {
        menuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        //MenuPanel.SetActive(!PlayerManager.player.playerControl);
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            menuPanel.SetActive(!menuPanel.activeSelf);
            GameObject.Find("GameManager").GetComponent<SaveScript>().Save();
            Gamemanager.main.player.playerControl = !Gamemanager.main.player.playerControl;
            hoverBox.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !Gamemanager.main.player.playerControl)
        {
            menuPanel.SetActive(false);
            GameObject.Find("GameManager").GetComponent<SaveScript>().Save();
            Gamemanager.main.player.playerControl = true;
            hoverBox.SetActive(false);
        }

    }

    public void navigateOnClick(GameObject content) {
        for (int i = 0; i< menuContents.Length; i++) {
            menuContents[i].gameObject.SetActive(false);
        }
        menuTitle.GetComponentInChildren<Text>().text = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        content.SetActive(true);
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
