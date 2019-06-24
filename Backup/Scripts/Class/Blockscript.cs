using UnityEngine;
using System.Collections;


[System.Serializable]
public class Blockscript {
    public int id;
    public string name;
    public string desc;
    public double health;
    public double minLayer;
    public double maxLayer;
    public double baseCash;
    public int xp;
    public double appearance;
    public Color32 color;
    public bool isInvisible, isOre, isLayerBlock, unBreakable, special;
    public Material material;

    /*
    public Blockscript(string name, double health, double minLayer, double maxLayer, double cash, int xp, double appearance, Color32 color, bool isInvisible, bool isOre, bool isLayerBlock, bool special, Material material) {
        this.name = name;
        this.health = health;
        this.minLayer = minLayer;
        this.maxLayer = maxLayer;
        this.cash = cash;
        this.xp = xp;
        this.appearance = appearance;
        this.color = color;
        this.isInvisible = isInvisible;
        this.isOre = isOre;
        this.isLayerBlock = isLayerBlock;
        this.special = special;
        this.material = material;

    } */
}
