using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {

	public Text message;
	GameObject canvas;

	// Use this for initialization
	void Start () {
		canvas = GameObject.FindGameObjectWithTag ("MainCanvas");
	}
	
	// Update is called once per frame
	void Update () {
		transform.SetParent(canvas.transform, false);
	}

	public void setMessage(string text, float duration)
	{
		message.text = text;
        GetComponent<Animator>().Play("MessageShow");
        StartCoroutine (Waiting (duration));
	}
	IEnumerator Waiting(float time)
	{
		yield return new WaitForSeconds(time);
        GetComponent<Animator>().Play("MessageHide");
		yield return new WaitForSeconds (1f);
		Destroy (this.gameObject);
	}
}
