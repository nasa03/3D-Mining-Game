using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Structure of Blocks
[System.Serializable]
public struct BlockInfo
{
    public int id;
    public string blockname;
    public string desc;
    public double health;
    public double cash;
    public int xp;
    public Color color;
    public Material material;
    public Texture texture;
    public bool isInvisible, unBreakable, isOre, isInteractable, isLayerBlock, hasMineEffect, disablePickup;

    public BlockInfo(
        int id, 
        string blockname,
        string desc,
        double health, 
        double cash, 
        int xp, 
        Color color, 
        Material material,
        Texture texture,
        bool isInvisible,
        bool unBreakable,
        bool isOre,
        bool isInteractable,
        bool isLayerBlock,
        bool hasMineEffect,
        bool disablePickup
        )

    {
        this.id = id;
        this.blockname = blockname;
        this.desc = desc;
        this.health = health;
        this.cash = cash;
        this.xp = xp;
        this.color = color;
        this.material = material;
        this.texture = texture;
        this.isInvisible = isInvisible;
        this.unBreakable = unBreakable;
        this.isOre = isOre;
        this.isInteractable = isInteractable;
        this.isLayerBlock = isLayerBlock;
        this.hasMineEffect = hasMineEffect;
        this.disablePickup = disablePickup;
    }
}
