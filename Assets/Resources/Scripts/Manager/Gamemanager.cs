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
    //List<BlockInfo> availableBlocks;
    BlockInfo[] availableBlocks;
    GameObject spawnedBlock;
    GameObject blockPrefab;
    Block currentMinedBlock;

    public List<Dimension> dimension;
    Dimension currentDimension;
    Dimensionlayer currentLayer;

    //Layer Management
    public Dictionary<int, double> blocksFromLayer = new Dictionary<int, double>();
    LayerBlock[] layerBlocks;

    //Game Flags
    public static bool[] GameFlags;

    // Tick
    private float tick = 1f;
    private float storeTick;
    public bool isTick;

    // Options
    public bool option_AutoClick;
    public bool option_AutoSell;
    public bool option_ShowTopUI;


    // UI
    public GameObject hoverBox;

    //RNG
    float random;
    private float weight;

    private void Start()
    {
        refreshBlockList((int)player.transform.position.y);
        for (int i = 0; i < blocksFromLayer.Count; i++)
        {
            weight += (float)blocksFromLayer.ElementAt(i).Value;
        }
    }

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        if (main == null)
            main = this;
        else if (main != this)
            Destroy(gameObject);

        // Initiate Game Variables
        InitScript init = new InitScript();
		availableBlocks = init.readBlocks();
        dimension = init.readDimension();
        blockPrefab = Resources.Load("Prefab/BlockPrefab") as GameObject;

        // Initiate Tick Variables
        storeTick = tick;
        isTick = false;

        currentDimension = dimension[0];

        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        //Perks.PerkSystem.InitPerks();
        GameObject.Find("GameManager").GetComponent<SaveScript>().StoreData();
    }

    //TODO: Save Flags
    void InitGameFlags()
    {

        /*   !!! WTF ARE Game Flags?! !!!
        *   
        *   Gameflags are booleans, they are used to identify certain rules, may it be tutorial stuff or part of a Perk.
        *   
        *   For example, if the flag in ID->0 is set to false, the tutorial seqment is going to be played. If the tutorial is done, ID->0 will be set to true.
        *   When the game is loaded again, the tutorial seqment will not be played.
        *  
        *   Check the Spreadsheet for the ID's
        *   
        *   W.I.P
        */

        GameFlags = new bool[1];
    }

    void Update()
    {
        TickUpdate();
    }

    // This Tick can be used on multiple scripts to manage updates in a least performance hitting scenario
    void TickUpdate()
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
    public GameObject SpawnBlock(int blockId, Vector3 whereToSpawn)
    {
               /* BlockInfo blockinfo = new BlockInfo(
                     block.id,
                     block.name,
                     block.health,
                     block.cash,
                     block.xp,
                     block.color,
                     block.material,
                     block.isInvisible,
                     block.unBreakable,
                     block.isOre,
                     block.isInteractable,
                     block.hasMineEffect,
                     block.disablePickup
                    ); */

                Block blockObject = blockPrefab.GetComponent<Block>();

                //blockObject.blockinfo = new BlockInfo();
                blockObject.blockinfo = availableBlocks[blockId];
                spawnedBlock = Instantiate(blockPrefab, whereToSpawn, Quaternion.identity) as GameObject;

        if (spawnedBlock == null)
        {
            Debug.Log("Block not spawned");
        }

        return spawnedBlock;
    }

    public void refreshBlockList(int playerCurrentLayer)
    {

        blocksFromLayer.Clear();

        currentLayer = currentDimension.getCurrentLayer(playerCurrentLayer);
        layerBlocks = currentDimension.getBlocksFromLayer(currentLayer);

        // * (!layerBlocks[i].ignoreLuck ? 1 + (player.stats["luck"].finalValue / 100) : 1)

        for (int i = 0; i < layerBlocks.Length; i++)
        {
            blocksFromLayer.Add(layerBlocks[i].blockId, layerBlocks[i].appearance );
        }
    }

    public GameObject SpawnRandomBlock()
    {
        return null;
    }

    public GameObject SpawnBlockByLayer(Vector3 whereToSpawn)
    {
        //random = Random.Range(0, 100);
        int randomIndex = Mathf.FloorToInt(Random.Range(0, blocksFromLayer.Count));
        GameObject spawnedBlock = null;

        float random = Random.Range(0, weight);
        weight = 0;

        for (int i = 0; i < blocksFromLayer.Count; i++)
        {
            if (random < blocksFromLayer.ElementAt(i).Value + weight)
            {
                if (spawnedBlock == null)
                    spawnedBlock = SpawnBlock(blocksFromLayer.ElementAt(i).Key, whereToSpawn);
            }
            weight += (float)blocksFromLayer.ElementAt(i).Value;
        }
        return spawnedBlock;
    }

    /* This replaces the previous block to a newer block*/
    public void replaceBlock(GameObject passedBlock, string name)
    {
        foreach (BlockInfo block in availableBlocks)
        {
            if (name == block.blockname)
            {
                Block replacedBlock = passedBlock.GetComponent<Block>();
                replacedBlock.blockinfo = block;
                /*Block replacedBlock = block.GetComponent<Block>();
                replacedBlock.blockname = _block.name;
                replacedBlock.health = _block.health;
                replacedBlock.color = _block.color;
                replacedBlock.isInvisible = _block.isInvisible;
                replacedBlock.baseCash = _block.baseCash;
                replacedBlock.material = _block.material;
                replacedBlock.unBreakable = _block.unBreakable;
                replacedBlock.isInteractable = _block.isInteractable;
                replacedBlock.hasMineEffect = _block.hasMineEffect;
                replacedBlock.disablePickup = _block.disablePickup;
                replacedBlock.xp = _block.xp;
                replacedBlock.isOre = _block.isOre;
                replacedBlock.ApplyBlockInfo(); */
            }
        }
    }

    /*public BlockInfo getBlock(string name)
    {
        foreach (BlockInfo block in availableBlocks)
        {
            if (block.blockname == name)
                return block;
        }
        return availableBlocks[0];
    } */

    public BlockInfo getBlock(int id)
    {
        if(availableBlocks[id].blockname == null)
        {
            return availableBlocks[0];
        }
        return availableBlocks[id];
    }

    public BlockInfo getBlock(string name)
    {
        foreach (BlockInfo block in availableBlocks)
        {
            if (block.blockname == name)
                return block;
        }
        return availableBlocks[0];
    }

    public BlockInfo[] getAllBlocks()
    {
        return availableBlocks.ToArray();
    }

    public int getAvailableOresCount()
    {
        return availableBlocks.Length;
    }

    public void option_enableAutoClick()
    {
        option_AutoClick = !option_AutoClick;
    }

    public void option_enableAutoSell()
    {
        option_AutoSell = !option_AutoSell;
    }

    public void option_enableShowTopUI()
    {
        option_ShowTopUI = !option_ShowTopUI;
        GameUITop.main.ToggleTopUI(option_ShowTopUI);
    }

    public void BlockisMined(GameObject block)
    {
        SpreadBlocks(block.transform.position, block);
    }

    public void SpreadBlocks(Vector3 position, GameObject block)
    {
        /*if(blockCreator.levelEditor)
        {
            return;
        } */

        Gamemanager.main.refreshBlockList((int)block.transform.position.y);
        //TODO Autodetect Intersect before its been created
        SpawnBlockByLayer(Vector3.left + position);
        SpawnBlockByLayer(Vector3.up + position);
        SpawnBlockByLayer(Vector3.forward + position);
        SpawnBlockByLayer(Vector3.right + position);
        SpawnBlockByLayer(Vector3.down + position);
        SpawnBlockByLayer(Vector3.back + position);
    }
}
