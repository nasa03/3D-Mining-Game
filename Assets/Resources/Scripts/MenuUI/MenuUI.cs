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
    public GameObject scrollContent;
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

    public void navigateOnClick(GameObject content) {
        for (int i = 0; i< menuContents.Length; i++) {
            menuContents[i].gameObject.SetActive(false);
        }
        //
        scrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -(350 - content.GetComponent<RectTransform>().sizeDelta.y));
        menuTitle.GetComponentInChildren<Text>().text = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        content.SetActive(true);
        AudioManager.main.Play("UI_Click", Random.Range(0.75f, 1.5f), gameObject, true);
    }

    public void onButtonHover(GameObject go)
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
