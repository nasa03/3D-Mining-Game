using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

    public float timeTillDestroy;
    bool isZero;
    // Use this for initialization
	void Start () {
        if(GetComponent<ParticleSystem>() != null)
        {
            timeTillDestroy = GetComponent<ParticleSystem>().duration;
        }
        if(timeTillDestroy == 0)
        {
            isZero = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!isZero)
        {
            timeTillDestroy -= Time.deltaTime;
            if (timeTillDestroy <= 0)
            {
                destroy();
            }
        }
	}

	public void destroy()
	{
		Destroy(this.gameObject);
	}
}
