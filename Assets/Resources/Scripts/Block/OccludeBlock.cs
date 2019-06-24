using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// W I P!!!!

public class OccludeBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {
            Debug.Log(other);
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Block")
            other.gameObject.SetActive(false);
    }
}
