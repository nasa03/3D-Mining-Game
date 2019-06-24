using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Dimension
{
    public string dimensionName;
    public string description;
    public layerStruct[] layers;

    public Dimension(string name, string desc, layerStruct[] layers)
    {
        dimensionName = name;
        description = desc;
        this.layers = layers;
    }
}

[System.Serializable]
public class layerStruct
{
    public int layer;
    public float radioactiveChance;
    public blockStruct[] blocks;

    public layerStruct(int layer, float radioactiveChance, blockStruct[] blocks)
    {
        this.layer = layer;
        this.radioactiveChance = radioactiveChance;
        this.blocks = blocks;
    }
}

[System.Serializable]
public struct blockStruct
{
    public int blockid;
    public double appearance;
}
