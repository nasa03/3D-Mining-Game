using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerDrillScript : MonoBehaviour {

    public DrillScript drill;
    public float speed;
    public int mineLevel;
    float storeSpeed;
    bool start;
    Collider[] coll;
    List<Collider> hits = new List<Collider>();

    void Start()
    {
    }

    void Update()
    {
        if(storeSpeed == 0)
        {
            storeSpeed = speed;
        }

        if (!drill.isMining)
        {
            fetchObject();
        }

        if (start)
        {
            drill.isMining = true;
            speed -= Time.deltaTime;
            if (hits[0] == null)
            {
                speed = storeSpeed;
                fetchObject();
            }
            if (speed <= 0)
            {
                speed = storeSpeed;
                hits[0].GetComponent<Block>().mineBlock();
                drill.durability -= 1;
                fetchObject();
            }
            
        }
        else
        {
            drill.isMining = false;
        }
    }

    void fetchObject()
    {
        //coll = Physics.OverlapSphere(transform.position, mineLevel);
        coll = Physics.OverlapBox(transform.position, new Vector3(mineLevel, mineLevel, mineLevel));
        hits.Clear();
        if(hits.Count == 0)
        {
            for (int i = 0; i<coll.Length; i++)
            {
                if(coll[i].tag == "Block" && !coll[i].GetComponent<Block>().isReplaced && !coll[i].GetComponent<Block>().unBreakable)
                {
                    if(drill.ignoreOres && !coll[i].GetComponent<Block>().isOre)
                    {
                        hits.Add(coll[i]);
                        start = true;
                    }
                    if(!drill.ignoreOres)
                    {
                        hits.Add(coll[i]);
                        start = true;
                    }
                    showHealthOnUI(drill.durability);   
                }
            }
        }
        if(hits.Count == 0)
        {
            start = false;
        }
    }

    void showHealthOnUI(float health)
    {
        GameObject.Find("DrillUI").GetComponent<DrillOverwatch>().drill = this.gameObject;
        GameObject.Find("DrillUI").GetComponent<DrillOverwatch>().text.text = health.ToString();
    }
}
