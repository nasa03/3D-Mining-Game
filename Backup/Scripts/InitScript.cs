using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class InitScript
{
    JsonData blockJson;


    void Start()
    {
        readBlocks();
    }

    public List<Blockscript> readBlocks()
    {
        List<Blockscript> availableBlocks = new List<Blockscript>();

        foreach (string file in Directory.GetFiles("Gamedata/Blocks"))
        {
            try
            {
                string words = File.ReadAllText(file);
                blockJson = JsonMapper.ToObject(words);
                availableBlocks.Add(writeBlock(blockJson));

            }
            catch (System.Exception e)
            {
                Debug.Log("Block " + file + " could not be loaded " + e);
            }
        }
        availableBlocks = availableBlocks.OrderBy(o => o.id).ToList();
        return availableBlocks;
    } 

    Blockscript writeBlock(JsonData info)
    {
        Blockscript _block = new Blockscript();
        _block.id = (int)info["id"];
        _block.name = info["name"].ToString();
        _block.desc = info["desc"].ToString();
        _block.health = (double)info["health"] / 10;
        _block.color = new Color32((byte)info["colorRed"], (byte)info["colorGreen"], (byte)info["colorBlue"], 255);
        _block.baseCash = (double)info["cash"] / 10;
        _block.minLayer = (double)info["minLayer"] / 10;
        _block.maxLayer = (double)info["maxLayer"] / 10;
        _block.xp = (int)info["xp"];
        _block.appearance = (double)info["appearance"] / 10;
        _block.material = Resources.Load("material/" + info["material"]) as Material;
        if (_block.material == null)
        {
            _block.material = Resources.Load("shader/BlockMaterial") as Material;
        }
        for (int i = 0; i < info["special"].Count; i++)
        {
            if (info["special"][i].ToString() == "isInvisible")
            {
                _block.isInvisible = true;
            }
            else if (info["special"][i].ToString() == "isLayerBlock")
            {
                _block.isLayerBlock = true;
            }
            else if (info["special"][i].ToString() == "unBreakable")
            {
                _block.unBreakable = true;
            }
            else if (info["special"][i].ToString() == "isOre")
            {
                _block.isOre = true;
            }
            else if (info["special"][i].ToString() == "surpriseCrate")
            {
                _block.special = true;
            }
            else
            {
                _block.isOre = true;
            }
        }
        return _block;

    }
}
