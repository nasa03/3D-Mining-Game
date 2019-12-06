using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// TODO: Remove this script and make a prefab out of this in the future

public class Blockmanager : MonoBehaviour {

    public static Blockmanager main = null;

    //Block Management
    BlockInfo[] availableBlocks;
    GameObject spawnedBlock;
    GameObject blockPrefab;
    Block currentMinedBlock;

    // Dimension
    public List<Dimension> dimension;
    Dimension currentDimension;
    Dimensionlayer currentLayer;
    public Dictionary<int, double> blocksFromLayer = new Dictionary<int, double>();
    LayerBlock[] layerBlocks;

    //RNG
    float random;
    private float weight;

    // Use this for initialization
    void Awake () {

        if (main == null)
            main = this;
        else if (main != this)
            Destroy(gameObject);

        JsonReader init = new JsonReader();
        availableBlocks = init.readBlocks();
        dimension = init.readDimension();
        blockPrefab = Resources.Load("Prefab/BlockPrefab") as GameObject;

        for (int i = 0; i < blocksFromLayer.Count; i++)
        {
            weight += (float)blocksFromLayer.ElementAt(i).Value;
        }


        currentDimension = dimension[0];
        RefreshBlockList(-15);
        CreateRoom(new Vector3(0, 15, 0), true);
    }

    public GameObject SpawnBlock(int blockId, Vector3 pos)
    {
        // Check if given position doesn't contain a block
        Collider[] hitCollider = Physics.OverlapSphere(pos, 0.1f);
        if (hitCollider.Length > 0)
        {
            return null;
        }

        Block blockObject = blockPrefab.GetComponent<Block>();
        blockObject.blockinfo = availableBlocks[blockId];

        spawnedBlock = Instantiate(blockPrefab, pos, Quaternion.identity) as GameObject;

        if (spawnedBlock == null)
        {
            Debug.Log("Block not spawned");
        }

        return spawnedBlock;
    }

    public void RefreshBlockList(int playerCurrentLayer)
    {

        blocksFromLayer.Clear();

        Debug.Log(currentDimension);

        currentLayer = currentDimension.getCurrentLayer(playerCurrentLayer);
        layerBlocks = currentDimension.getBlocksFromLayer(currentLayer);

        // * (!layerBlocks[i].ignoreLuck ? 1 + (player.stats["luck"].finalValue / 100) : 1)

        for (int i = 0; i < layerBlocks.Length; i++)
        {
            blocksFromLayer.Add(layerBlocks[i].blockId, layerBlocks[i].appearance);
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

    /* This replaces the current block to a different block*/
    public void ReplaceBlock(GameObject passedBlock, string name)
    {
        foreach (BlockInfo block in availableBlocks)
        {
            if (name.Equals(block.blockname))
            {
                Block replacedBlock = passedBlock.GetComponent<Block>();
                replacedBlock.blockinfo = block;
            }
        }
    }

    public BlockInfo GetBlock(int id)
    {
        if (availableBlocks[id].blockname == null)
        {
            return availableBlocks[0];
        }
        return availableBlocks[id];
    }

    public BlockInfo GetBlock(string name)
    {
        foreach (BlockInfo block in availableBlocks)
        {
            if (block.blockname == name)
                return block;
        }
        return availableBlocks[0];
    }

    public int GetBlockID(string name)
    {
        foreach (BlockInfo block in availableBlocks)
        {
            if (block.blockname == name)
                return block.id;
        }
        return availableBlocks[0].id;
    }

    public BlockInfo[] GetAllBlocks()
    {
        return availableBlocks.ToArray();
    }

    public int GetAvailableOresCount()
    {
        return availableBlocks.Length;
    }

    public void BlockisMined(GameObject block)
    {
        SpreadBlocks(block.transform.position, block);
    }

    public void SpreadBlocks(Vector3 position, GameObject block)
    {
        RefreshBlockList((int)block.transform.position.y);

        SpawnBlockByLayer(Vector3.left + position);
        SpawnBlockByLayer(Vector3.up + position);
        SpawnBlockByLayer(Vector3.forward + position);
        SpawnBlockByLayer(Vector3.right + position);
        SpawnBlockByLayer(Vector3.down + position);
        SpawnBlockByLayer(Vector3.back + position);
    }

    //TODO Block Prefab
    void CreateRoom(Vector3 positionToSpawn, bool isPlayerSpawn)
    {
        RefreshBlockList((int)positionToSpawn.y);
        SpawnBlockByLayer(new Vector3(0.0f, 0.0f, 0.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-1.0f, 0.0f, 0.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-2.0f, 0.0f, 0.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-3.0f, 0.0f, 0.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(0.0f, 0.0f, -1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-1.0f, 0.0f, -1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-2.0f, 0.0f, -1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-3.0f, 0.0f, -1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-3.0f, 0.0f, -2.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-2.0f, 0.0f, -2.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-1.0f, 0.0f, -2.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(0.0f, 0.0f, -2.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(0.0f, 0.0f, -3.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-1.0f, 0.0f, -3.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-3.0f, 0.0f, -3.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-2.0f, 0.0f, -3.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-4.0f, 0.0f, 0.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-4.0f, 0.0f, -2.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-4.0f, 0.0f, -1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-4.0f, 0.0f, -3.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-4.0f, 0.0f, -4.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-3.0f, 0.0f, -4.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-2.0f, 0.0f, -4.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(0.0f, 0.0f, -4.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-1.0f, 0.0f, -4.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(0.0f, 1.0f, 0.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(0.0f, 2.0f, 0.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(0.0f, 3.0f, 0.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-5.0f, 0.0f, 0.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-5.0f, 0.0f, -1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-5.0f, 0.0f, -2.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-5.0f, 0.0f, -3.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-5.0f, 0.0f, -4.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-6.0f, 0.0f, 0.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-6.0f, 0.0f, -1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-6.0f, 0.0f, -2.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-6.0f, 0.0f, -3.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-6.0f, 0.0f, -4.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(0.0f, 0.0f, -5.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-1.0f, 0.0f, -5.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-2.0f, 0.0f, -5.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-3.0f, 0.0f, -5.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-4.0f, 0.0f, -5.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-5.0f, 0.0f, -5.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-6.0f, 0.0f, -5.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-6.0f, 1.0f, 0.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-6.0f, 2.0f, 0.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-6.0f, 3.0f, 0.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(0.0f, 0.0f, -6.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-1.0f, 0.0f, -6.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-2.0f, 0.0f, -6.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-3.0f, 0.0f, -6.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-4.0f, 0.0f, -6.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-5.0f, 0.0f, -6.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-6.0f, 0.0f, -6.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-6.0f, 1.0f, -6.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-6.0f, 2.0f, -6.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-6.0f, 3.0f, -6.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(0.0f, 1.0f, -6.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(0.0f, 2.0f, -6.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(0.0f, 3.0f, -6.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-5.0f, 1.0f, -7.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-5.0f, 2.0f, -7.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-5.0f, 3.0f, -7.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-4.0f, 1.0f, -7.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-3.0f, 1.0f, -7.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-2.0f, 1.0f, -7.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-1.0f, 1.0f, -7.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-4.0f, 2.0f, -7.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-3.0f, 2.0f, -7.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-2.0f, 2.0f, -7.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-1.0f, 2.0f, -7.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-4.0f, 3.0f, -7.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-3.0f, 3.0f, -7.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-2.0f, 3.0f, -7.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-1.0f, 3.0f, -7.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-7.0f, 1.0f, -5.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-7.0f, 1.0f, -4.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-7.0f, 1.0f, -3.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-7.0f, 1.0f, -2.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-7.0f, 1.0f, -1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-7.0f, 2.0f, -5.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-7.0f, 3.0f, -5.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-7.0f, 2.0f, -4.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-7.0f, 3.0f, -4.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-7.0f, 2.0f, -3.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-7.0f, 3.0f, -3.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-7.0f, 2.0f, -1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-7.0f, 2.0f, -2.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-7.0f, 3.0f, -1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-7.0f, 3.0f, -2.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-5.0f, 1.0f, 1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-5.0f, 2.0f, 1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-5.0f, 3.0f, 1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-4.0f, 1.0f, 1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-4.0f, 2.0f, 1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-4.0f, 3.0f, 1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-3.0f, 3.0f, 1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-3.0f, 2.0f, 1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-3.0f, 1.0f, 1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-2.0f, 1.0f, 1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-2.0f, 2.0f, 1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-2.0f, 3.0f, 1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-1.0f, 1.0f, 1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-1.0f, 2.0f, 1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(-1.0f, 3.0f, 1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(1.0f, 1.0f, -1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(1.0f, 2.0f, -1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(1.0f, 3.0f, -1.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(1.0f, 1.0f, -2.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(1.0f, 2.0f, -2.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(1.0f, 3.0f, -2.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(1.0f, 3.0f, -3.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(1.0f, 2.0f, -3.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(1.0f, 1.0f, -3.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(1.0f, 1.0f, -4.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(1.0f, 1.0f, -5.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(1.0f, 2.0f, -4.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(1.0f, 2.0f, -5.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(1.0f, 3.0f, -4.0f) + positionToSpawn);
        SpawnBlockByLayer(new Vector3(1.0f, 3.0f, -5.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-6.0f, 4.0f, -5.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-5.0f, 4.0f, -5.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-4.0f, 4.0f, -5.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-3.0f, 4.0f, -5.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-2.0f, 4.0f, -5.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-1.0f, 4.0f, -5.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(0.0f, 4.0f, -5.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(0.0f, 4.0f, -4.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-1.0f, 4.0f, -4.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-2.0f, 4.0f, -4.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-3.0f, 4.0f, -4.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-4.0f, 4.0f, -4.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-5.0f, 4.0f, -4.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-6.0f, 4.0f, -4.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-6.0f, 4.0f, -3.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-5.0f, 4.0f, -3.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-4.0f, 4.0f, -3.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-3.0f, 4.0f, -3.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-2.0f, 4.0f, -3.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(0.0f, 4.0f, -3.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-1.0f, 4.0f, -3.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(0.0f, 4.0f, -2.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-1.0f, 4.0f, -2.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-2.0f, 4.0f, -2.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-3.0f, 4.0f, -2.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-4.0f, 4.0f, -2.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-5.0f, 4.0f, -2.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-6.0f, 4.0f, -2.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(0.0f, 4.0f, -1.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-1.0f, 4.0f, -1.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-2.0f, 4.0f, -1.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-3.0f, 4.0f, -1.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-4.0f, 4.0f, -1.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-5.0f, 4.0f, -1.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-6.0f, 4.0f, -1.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-5.0f, 4.0f, 0.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-4.0f, 4.0f, 0.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-3.0f, 4.0f, 0.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-2.0f, 4.0f, 0.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-1.0f, 4.0f, 0.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-1.0f, 4.0f, -6.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-2.0f, 4.0f, -6.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-3.0f, 4.0f, -6.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-4.0f, 4.0f, -6.0f) + positionToSpawn);
        SpawnBlock(1, new Vector3(-5.0f, 4.0f, -6.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-1.0f, 1.0f, -6.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-1.0f, 2.0f, -6.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-1.0f, 3.0f, -6.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-2.0f, 3.0f, -6.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-2.0f, 2.0f, -6.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-2.0f, 1.0f, -6.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-3.0f, 1.0f, -6.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-3.0f, 2.0f, -6.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-3.0f, 3.0f, -6.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-4.0f, 3.0f, -6.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-4.0f, 1.0f, -6.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-4.0f, 2.0f, -6.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-5.0f, 1.0f, -6.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-5.0f, 2.0f, -6.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-5.0f, 3.0f, -6.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-6.0f, 3.0f, -5.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-6.0f, 2.0f, -5.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-6.0f, 1.0f, -5.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-6.0f, 1.0f, -4.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-6.0f, 2.0f, -4.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-6.0f, 3.0f, -4.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-6.0f, 3.0f, -3.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-6.0f, 2.0f, -3.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-6.0f, 1.0f, -3.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-6.0f, 1.0f, -2.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-6.0f, 2.0f, -2.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-6.0f, 3.0f, -2.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-6.0f, 3.0f, -1.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-6.0f, 2.0f, -1.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-6.0f, 1.0f, -1.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-5.0f, 1.0f, 0.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-5.0f, 2.0f, 0.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-5.0f, 3.0f, 0.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-4.0f, 3.0f, 0.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-4.0f, 2.0f, 0.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-4.0f, 1.0f, 0.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-3.0f, 1.0f, 0.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-3.0f, 2.0f, 0.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-3.0f, 3.0f, 0.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-2.0f, 3.0f, 0.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-2.0f, 2.0f, 0.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-2.0f, 1.0f, 0.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-1.0f, 1.0f, 0.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-1.0f, 2.0f, 0.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-1.0f, 3.0f, 0.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(0.0f, 3.0f, -1.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(0.0f, 2.0f, -1.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(0.0f, 1.0f, -1.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(0.0f, 1.0f, -2.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(0.0f, 2.0f, -2.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(0.0f, 3.0f, -2.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(0.0f, 3.0f, -3.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(0.0f, 2.0f, -3.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(0.0f, 1.0f, -3.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(0.0f, 1.0f, -4.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(0.0f, 2.0f, -4.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(0.0f, 3.0f, -4.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(0.0f, 3.0f, -5.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(0.0f, 2.0f, -5.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(0.0f, 1.0f, -5.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-5.0f, 1.0f, -5.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-5.0f, 1.0f, -4.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-5.0f, 1.0f, -3.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-5.0f, 1.0f, -1.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-5.0f, 1.0f, -2.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-4.0f, 1.0f, -1.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-3.0f, 1.0f, -1.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-1.0f, 1.0f, -1.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-2.0f, 1.0f, -1.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-1.0f, 1.0f, -2.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-1.0f, 1.0f, -3.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-1.0f, 1.0f, -5.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-1.0f, 1.0f, -4.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-2.0f, 1.0f, -5.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-3.0f, 1.0f, -5.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-4.0f, 1.0f, -5.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-4.0f, 1.0f, -4.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-4.0f, 1.0f, -3.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-4.0f, 1.0f, -2.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-3.0f, 1.0f, -2.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-2.0f, 1.0f, -2.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-2.0f, 1.0f, -3.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-2.0f, 1.0f, -4.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-3.0f, 1.0f, -3.0f) + positionToSpawn);
        SpawnBlock(0, new Vector3(-3.0f, 1.0f, -4.0f) + positionToSpawn);


        if (isPlayerSpawn)
        {
            GameObject.Find("Player").transform.position = positionToSpawn + new Vector3(-3f, 0, -3);
        }
    }
}
