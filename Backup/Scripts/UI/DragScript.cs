using UnityEngine;
using System.Collections;

public class DragScript : MonoBehaviour {

    public GameObject dragPlace;

    public void Dragging()
    {
        dragPlace.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y - dragPlace.GetComponent<RectTransform>().sizeDelta.y / 2, Input.mousePosition.z) ;
        dragPlace.transform.SetAsLastSibling();
    }
}
