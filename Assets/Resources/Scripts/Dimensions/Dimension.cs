using System.Collections;
using System.Collections.Generic;

// Dimension Manager.

[System.Serializable]
public class Dimension
{
    public int id;
    public string dimensionName;
    public string description;
    public Dimensionlayer[] layers;

    public Dimension(int id, string name, string desc, Dimensionlayer[] layers)
    {
        this.id = id;
        dimensionName = name;
        description = desc;
        this.layers = layers;
    }

    //Gets its current position layer based on players Y position. Fallback to the first layer, if no other layer exist afterwards
    public Dimensionlayer getCurrentLayer(int playerPosition)
    {
        for(int i = 0; i<layers.Length; i++)
        {
            if(layers[i].layer <= -playerPosition)
            {
                if(layers[i].maxLayer >= -playerPosition)
                {
                    return layers[i];
                }
            }
        }
        return layers[0];
    }

    // Returns the blocks that are within that layer
    public LayerBlock[] getBlocksFromLayer(Dimensionlayer layer)
    {
        return layer.blocks;
    }
}

[System.Serializable]
public struct Dimensionlayer
{
    public int layer;
    public int maxLayer;
    public int radioactiveChance;
    public LayerBlock[] blocks;

    public Dimensionlayer(int layer, int maxLayer, int radioactiveChance, LayerBlock[] blocks)
    {
        this.layer = layer;
        this.maxLayer = maxLayer;
        this.radioactiveChance = radioactiveChance;
        this.blocks = blocks;
    }
}

[System.Serializable]
public struct LayerBlock
{
    public int blockId;
    public double appearance;
    public bool ignoreLuck;
    public LayerBlock(int blockId, double appearance, bool ignoreLuck)
    {
        this.blockId = blockId;
        this.appearance = appearance;
        this.ignoreLuck = ignoreLuck;
    }
}
