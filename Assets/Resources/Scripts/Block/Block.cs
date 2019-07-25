using UnityEngine;
using System.Collections;
using System;

// Block Component. In order to reduce RAM usage, it's adviced to not create multiple script components on the Block prefabs

public class Block : MonoBehaviour {

    public BlockInfo blockinfo;
    public double savedHealth;
    public Renderer blockRenderer;
    public MaterialPropertyBlock blockColor;

	// Init
    void Start () {
      	blockRenderer = GetComponent<Renderer>();
       	blockColor = new MaterialPropertyBlock();
       	ApplyBlockInfo();
    }

    // Check if other blocks are intersecting. If it does, delete this block.
	void Awake () {
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
		
		for(int i = 0; i<hitColliders.Length; i++)
		{
            if (hitColliders[i].transform.root != transform)
			{
				if(hitColliders[i].bounds.Intersects(gameObject.GetComponent<Renderer>().bounds))
				{
                    //Debug.Log("Intersecting with " + hitColliders[i].GetComponent<Block>().blockname);
					Destroy(gameObject);
				}
			}
		}
	}

    // Assign properties to block
	public void ApplyBlockInfo()
	{
        savedHealth = blockinfo.health;
        blockRenderer.sharedMaterial = blockinfo.material;

        if(blockinfo.texture != null)
        {
            blockColor.SetTexture("_BaseMap", blockinfo.texture);
        }

        blockColor.SetColor("_BaseColor", blockinfo.color);
        blockRenderer.SetPropertyBlock(blockColor);

        //Debug.Log(blockRenderer.sharedMaterial.GetTexture("_BaseMap"));

        if (blockinfo.isInvisible) {
            blockRenderer.enabled = false;
			gameObject.GetComponent<BoxCollider>().isTrigger = true;
			gameObject.layer = 2;
		}
    }
	
    // Block gets damaged
    public void HurtBlock(float damage)
    {
        if (blockinfo.unBreakable)
            return;

        AudioManager.main.Play("Block_Hit", UnityEngine.Random.Range(0.75f, 1.5f),gameObject,false);
        blockinfo.health -= damage;

        if (blockinfo.health <= 0)
            MineBlock();
    }

    //Block gets mined
    public void MineBlock()
    {
        GameObject partic = Instantiate(Resources.Load("Prefab/BlockDestroyParticle"), transform.position, Quaternion.identity) as GameObject;

        // If Block Pickup is enabled
        if (!blockinfo.disablePickup)
        {

            InventoryItem item = Inventory.main.AddInventory(blockinfo.id.ToString(), 1, ItemType.Block);

            if (Gamemanager.main.option_AutoSell)
            {
                Inventory.main.SellItem(item, 1);
            }
                
        }

        // Apply Block Effect if it has an Use effect
        if (blockinfo.isInteractable && Gamemanager.main.option_AutoSell)
        {
            BuffManager.main.useBlockBuff(blockinfo.id);
        }

        // Apply Block Effect if it has a Mine effect
        if (blockinfo.hasMineEffect)
        {
            BuffManager.main.OnMinedBlock(blockinfo.id);
        }

        Gamemanager.main.player.xp.GiveXP(blockinfo.xp);
        //partic.GetComponent<ParticleSystem>().startColor = new Color(this.color.r, this.color.g, this.color.b, 1);

        Gamemanager.main.ReplaceBlock(gameObject, "Air");
        ApplyBlockInfo();
        Gamemanager.main.BlockisMined(this.gameObject);
        gameObject.tag = "Air";

        //Destroy Renderer to save up RAM
        Destroy(blockRenderer);

        //Destroy script to save up RAM
        Destroy(this);
    }
}
