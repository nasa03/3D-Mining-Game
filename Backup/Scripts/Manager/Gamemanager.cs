using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    //Accessable Classes
    public static Gamemanager main = null;
    public PlayerScript player;
    public DialogueBox dialogue;

    //Block Management
    List<Blockscript> availableBlocks;
    GameObject spawnedBlock;
    GameObject blockPrefab;

    public List<Dimension> dimension;

    //Layer Management
    Dictionary<string, double> blocksFromLayer = new Dictionary<string, double>();
    string blockLayer;

    // Tick
    private float tick = 1f;
    private float storeTick;
    public bool isTick;

    // UI
    public GameObject hoverBox;

    //RNG
    float random;
    private float weight;

    private void Start()
    {
        for (int i = 0; i < blocksFromLayer.Count; i++)
        {
            weight += (float)blocksFromLayer.ElementAt(i).Value;
        }
    }

    void Awake() {
        if (main == null)
            main = this;
        else if (main != this)
            Destroy(gameObject);

        // Initiate Game Variables
        InitScript init = new InitScript();
        blockPrefab = Resources.Load("Prefab/BlockPrefab") as GameObject;

        // Initiate Tick Variables
        storeTick = tick;
        isTick = false;

        availableBlocks = init.readBlocks();

        player = GameObject.Find("Player").GetComponent<PlayerScript>();

        GameObject.Find("GameManager").GetComponent<SaveScript>().Load();
    }

    void Update()
    {
        tick -= Time.deltaTime;
        if (tick <= 0)
        {
            isTick = (tick <= 0 ? true : false);
            tick = storeTick;
        }
            
    }

    /*
     This manages the way blocks are spawned.     
    */
    public GameObject SpawnBlock(string blockName, Vector3 whereToSpawn) {
        foreach (Blockscript _block in availableBlocks) {
            if (_block.name == blockName) {

                Block tempBlockScript = blockPrefab.GetComponent<Block>();
                tempBlockScript.id = _block.id;
                tempBlockScript.blockname = _block.name;
                tempBlockScript.health = _block.health;
                tempBlockScript.color = _block.color;
                tempBlockScript.isInvisible = _block.isInvisible;
                tempBlockScript.baseCash = _block.baseCash;
                tempBlockScript.material = _block.material;
                tempBlockScript.unBreakable = _block.unBreakable;
                tempBlockScript.isSpecial = _block.special;
                tempBlockScript.isOre = _block.isOre;
                tempBlockScript.xp = _block.xp;

                spawnedBlock = Instantiate(blockPrefab, whereToSpawn, Quaternion.identity) as GameObject;
            }
        }

        if (spawnedBlock == null)
        {
            Debug.Log("Block not spawned");
        }

        return spawnedBlock;
    }

    public void refreshBlockList(double currentLayer) {

        blocksFromLayer.Clear();

        foreach (Blockscript block in availableBlocks) {
            if(block.minLayer <= -currentLayer) {
                if(block.maxLayer >= -currentLayer) {

                    if(block.isLayerBlock) {
                        blockLayer = block.name;
                        continue;
                    }
                    blocksFromLayer.Add(block.name, block.appearance);
                }
            }
        }

        if (blockLayer == null)
        {
            blockLayer = "Dirt";
        }
    }

    public GameObject SpawnRandomBlock() {
        return null;
    }

    public GameObject SpawnBlockByLayer(Vector3 whereToSpawn) {
        //random = Random.Range(0, 100);
        int randomIndex = Mathf.FloorToInt(Random.Range(0, blocksFromLayer.Count));
        GameObject spawnedBlock;

        float random = Random.Range(0, 600 + weight);
        weight = 0;
  
        for(int i = 0; i< blocksFromLayer.Count; i++)
        {
            if (random < blocksFromLayer.ElementAt(i).Value + player.stats["luck"].finalValue + weight)
            {
                spawnedBlock = SpawnBlock(blocksFromLayer.ElementAt(i).Key, whereToSpawn);
            }
            weight += (float)blocksFromLayer.ElementAt(i).Value;
        }

        spawnedBlock = SpawnBlock(blockLayer, whereToSpawn);

        return spawnedBlock;
    }

    /* This replaces the previous block to a newer block*/
    public void replaceBlock(GameObject block, string name) {

        foreach (Blockscript _block in availableBlocks)
        {
            if (name == _block.name)
            {
                Block replacedBlock = block.GetComponent<Block>();
                replacedBlock.blockname = _block.name;
                replacedBlock.health = _block.health;
                replacedBlock.color = _block.color;
                replacedBlock.isInvisible = _block.isInvisible;
                replacedBlock.baseCash = _block.baseCash;
                replacedBlock.material = _block.material;
                replacedBlock.unBreakable = _block.unBreakable;
                replacedBlock.isSpecial = _block.special;
                replacedBlock.xp = _block.xp;
                replacedBlock.isOre = _block.isOre;
                replacedBlock.ApplyBlockInfo();
            }
        }
    }

    public Blockscript getBlock(string name)
    {

        foreach (Blockscript _block in availableBlocks)
        {
            if (_block.name == name)
                return _block;
        }
        return null;
    }

    public Blockscript getBlock(string name, double layer)
    {
        foreach (Blockscript _block in availableBlocks)
        {
            if (_block.name == name)
                return _block;
        }
        return null;
    }

    public Blockscript[] getAllBlocks() {
        return availableBlocks.ToArray();
    }

    public string getLayerText(int height)
    {
        foreach (Blockscript name in availableBlocks)
        {
            if (name.isLayerBlock)
            {
                if (name.maxLayer >= height)
                {
                    if (name.minLayer <= height)
                    {
                        return name.name;
                    }
                }
            }
        }
        return "";
    }

    public int getAvailableOresCount () {
        return availableBlocks.Count;
    }
}
