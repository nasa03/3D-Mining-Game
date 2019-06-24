using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class panelScript : MonoBehaviour {

    public bool isClosed;
    public bool isSticked;
    public GameObject StickButton;

    public void closePanel()
    {
        isClosed = true;
        isSticked = false;
        gameObject.SetActive(false);
    }

    public void openPanel()
    {
        isClosed = false;
        gameObject.SetActive(true);
        gameObject.transform.SetAsLastSibling();
    }

    public void stickPanel()
    {
        isSticked = !isSticked;
        if(isSticked)
        {
            StickButton.GetComponentInChildren<Text>().text = "1";
        }
        else
        {
            StickButton.GetComponentInChildren<Text>().text = "0";
        }
    }

}
