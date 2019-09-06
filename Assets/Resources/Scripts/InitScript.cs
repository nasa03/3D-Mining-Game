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

    public BlockInfo[] readBlocks()
    {
        BlockInfo[] availableBlocks = new BlockInfo[128];

        foreach (string file in Directory.GetFiles("Gamedata/Blocks"))
        {
            try
            {
                string words = File.ReadAllText(file);
                blockJson = JsonMapper.ToObject(words);

                BlockInfo tempBlock = writeBlock(blockJson);
                availableBlocks[tempBlock.id] = tempBlock;
            }
            catch (System.Exception e)
            {
                Debug.Log("Block " + file + " could not be loaded " + e);
            }
        }

        //availableBlocks = availableBlocks.OrderBy(o => o.id).ToList();
        return availableBlocks;
    }

    public List<Dimension> readDimension()
    {
        List<Dimension> availableDimensions = new List<Dimension>();

        foreach (string file in Directory.GetFiles("Gamedata/Dimensions"))
        {
            try
            {
                string words = File.ReadAllText(file);
                blockJson = JsonMapper.ToObject(words);
                availableDimensions.Add(writeDimension(blockJson));

            }
            catch (System.Exception e)
            {
                Debug.Log("Dimension " + file + " could not be loaded " + e);
            }
        }
        availableDimensions = availableDimensions.OrderBy(o => o.id).ToList();
        return availableDimensions;
    }

    private BlockInfo writeBlock(JsonData info)
    {
        BlockInfo _block = new BlockInfo();
        _block.id = (int)info["id"];
        _block.blockname = info["name"].ToString();
        _block.desc = info["desc"].ToString();
        _block.health = (double)info["health"];
        _block.color = new Color32((byte)info["colorRed"], (byte)info["colorGreen"], (byte)info["colorBlue"], 255);
        _block.cash = (double)info["cash"];
        _block.xp = (int)info["xp"];
        _block.material = Resources.Load("material/" + info["material"]) as Material;
        if(info["texture"].ToString() != "none")
        {
            //_block.texture = Resources.Load("texture/" + info["texture"]) as Texture;
            Texture2D tex = new Texture2D(128,128);
            ImageConversion.LoadImage(tex, File.ReadAllBytes("Gamedata/Textures/" + info["texture"].ToString() + ".png"));
            _block.texture = tex;
        }
       
        if (_block.material == null)
        {
            _block.material = Resources.Load("shader/BlockMaterial") as Material;
        }
        for (int i = 0; i < info["special"].Count; i++)
        {
            switch (info["special"][i].ToString())
            {
                case "isInvisible":
                    _block.isInvisible = true;
                    break;

                case "isLayerBlock":
                    _block.isLayerBlock = true;
                    break;

                case "unBreakable":
                    _block.unBreakable = true;
                    break;

                case "isOre":
                    _block.isOre = true;
                    break;

                case "isInteractable":
                    _block.isInteractable = true;
                    break;

                case "hasMineEffect":
                    _block.hasMineEffect = true;
                    break;

                case "disablePickup":
                    _block.disablePickup = true;
                    break;

                default:
                    _block.isOre = true;
                    Debug.LogError("No Special Attribute of name '" + info["special"][i].ToString() + "' found!");
                    break;
            }
        }
        return _block;

    }

    private Dimension writeDimension(JsonData info)
    {
        Dimension jsonDimension = new Dimension(
            (int)info["id"],
            info["name"].ToString(),
            info["desc"].ToString(),
            new Dimensionlayer[info["layers"].Count]
            );

        for (int i = 0; i < info["layers"].Count; i++)
        {
            jsonDimension.layers[i] = new Dimensionlayer(
                (int)info["layers"][i]["minLayer"],
                (int)info["layers"][i]["maxLayer"],
                (int)info["layers"][i]["radioactivity"],
                new LayerBlock[info["layers"][i]["blocks"].Count]);

            for (int o = 0; o < info["layers"][i]["blocks"].Count; o++)
            {
                jsonDimension.layers[i].blocks[o] = new LayerBlock(
                    (int)info["layers"][i]["blocks"][o]["block"],
                    (double)info["layers"][i]["blocks"][o]["appearance"],
                    (bool)info["layers"][i]["blocks"][o]["ignoreLuck"]
                    );
            }

        }
        return jsonDimension;
    }
}
