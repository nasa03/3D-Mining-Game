using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string text;
    public Vector2 scale;

    float scaleFactor;
    GameObject hoverBox;

    Vector2 pos;
    Canvas canvas;
    RectTransform hoverBoxTransform;
    Text uiText;

    // Start is called before the first frame update
    void Start() {
        hoverBox = Gamemanager.main.hoverBox;
        hoverBoxTransform = hoverBox.GetComponent<RectTransform>();
        uiText = hoverBox.GetComponentInChildren<Text>();
        canvas = hoverBox.GetComponentInParent<Canvas>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(hoverBox.activeSelf) {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
            hoverBox.transform.position = canvas.transform.TransformPoint(new Vector2(25 + pos.x + hoverBoxTransform.sizeDelta.x / 2, pos.y - 25));
        }
    }

    public void display(string text, Vector2 scale) {
        uiText.text = text;
        hoverBoxTransform.sizeDelta = new Vector2(250 + scale.x, 32 + scale.y);
    }

    public void setDisplay(string text, Vector2 scale) {
        this.text = text;
        this.scale = scale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverBox.SetActive(true);
        display(text, scale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverBox.SetActive(false);
    }
}
