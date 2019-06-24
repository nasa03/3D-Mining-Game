using UnityEngine;
using System.Collections;
using System;

public class Block : MonoBehaviour {

    public int id;
    public string blockname;
	public double health, savedHealth;
    public int xp;
	public Color color;
	public bool isInvisible, unBreakable, isOre;
    public bool isSpecial { get; set; }
	public double baseCash;
	public Material material;
	Renderer blockMaterial;
	public bool isReplaced;
    bool originColor = true;
    Color currentColor;
    Blockmanager blockManager;
    float timeColor;

	// Use this for initialization
	void Start () {
		blockManager = GameObject.Find ("GameManager").GetComponent<Blockmanager> ();
        blockMaterial = GetComponent<Renderer>();
        ApplyBlockInfo();
        makeBlockBlink();
	}

    void Update()
    {
        if(!originColor)
        {
            currentColor = Color.Lerp(Color.white, color, timeColor);
            timeColor += Time.deltaTime;
            blockMaterial.material.SetColor("_BaseColor", currentColor);
            if (timeColor >= 1)
            {
                originColor = true;
                timeColor = 1;
            }
        }
    }

	void Awake () {
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
		
		for(int i = 0; i<hitColliders.Length; i++)
		{
			if(hitColliders[i].transform.root != transform)
			{
				if(hitColliders[i].bounds.Intersects(gameObject.GetComponent<Renderer>().bounds))
				{
                    //Debug.Log("Intersecting with " + hitColliders[i].GetComponent<Block>().blockname);
                    //Destroy(hitColliders[i].gameObject);
					Destroy(gameObject);
				}
			}
		}
	}

	public void hurtBlock(float pickLevel)
	{

        if(unBreakable) {
            return;
        }

		health -= pickLevel;
		if (health <= 0) {
            GameObject partic = Instantiate(Resources.Load("Prefab/BlockDestroyParticle"), transform.position, Quaternion.identity) as GameObject;

            GameObject.Find("GameManager").GetComponent<InventorySystem>().AddInventoryFromBlock(this, 1);
                
            partic.GetComponent<ParticleSystem>().startColor = new Color(this.color.r, this.color.g, this.color.b, 1);
            //getCashFromBlock (gameObject);
            //GameObject.Find("Player").GetComponent<XPSystem>().getXP(this);
			Gamemanager.main.replaceBlock (gameObject, "Air");
            ApplyBlockInfo();
            isReplaced = true;
			blockManager.BlockisMined (this.gameObject);
		    //Destroy(gameObject);
		}
        makeBlockBlink();
	}

    void makeBlockBlink()
    {
        originColor = false;
        timeColor = 0;
    }

	public void mineBlock()
	{
        Gamemanager.main.replaceBlock(gameObject, "Air");
        ApplyBlockInfo();
        isReplaced = true;
		blockManager.BlockisMined (this.gameObject);
    }

	public void ApplyBlockInfo()
	{

        savedHealth = health;
		blockMaterial.material = material;
        //blockMaterial.material.color = color;
        blockMaterial.material.SetColor("_BaseColor", color);
		
		if (isInvisible) {
			blockMaterial.enabled = false;
			gameObject.GetComponent<BoxCollider>().isTrigger = true;
			gameObject.layer = 2;
		}

        if(isSpecial) {
            gameObject.AddComponent(Type.GetType("SpecialBlocks"));
        }
    }
}
