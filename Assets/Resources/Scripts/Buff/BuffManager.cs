using BuffEffect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Buff Manager. Handles all the effects that Blocks carry.

namespace BuffEffect { 
    public enum BuffTypeEnum { OnOpen, OnMined, Crate};

    public abstract class BuffEffectClass
    {
        public abstract string BuffEffectName { get; protected set; }
        public abstract float BuffEffectChance { get; protected set; }
        public abstract bool IgnoreLuck { get; protected set; }
        public abstract BuffTypeEnum BuffType { get; protected set; }

        public void AddToList()
        {
            switch (BuffType)
            {
                case BuffTypeEnum.Crate:
                    BuffManager.main.currentCrateBuffs.Add(BuffEffectName, this);
                    BuffManager.main.maxCrateWeight += BuffEffectChance;
                    break;

                case BuffTypeEnum.OnMined:
                    BuffManager.main.currentOnMinedBuffs.Add(BuffEffectName, this);
                    break;

                case BuffTypeEnum.OnOpen:
                    BuffManager.main.currentOnOpenBuffs.Add(BuffEffectName, this);
                    break;
            }
                //BuffManager.main.currentBuffEffects.Add(BuffEffectName, this);
        }
        public abstract void applyBuff(PlayerScript player, string[] args);
    }
}

public class BuffManager : MonoBehaviour
{
    public static BuffManager main = null;
    public Dictionary<string, BuffEffectClass> currentOnMinedBuffs { get; private set; }
    public Dictionary<string, BuffEffectClass> currentOnOpenBuffs { get; private set; }
    public Dictionary<string, BuffEffectClass> currentCrateBuffs { get; private set; }
    public float maxCrateWeight;

    //public Dictionary<string, BuffEffectClass> currentBuffEffects = new Dictionary<string, BuffEffectClass>();

    void Awake()
    {
        if (main == null)
            main = this;
        else if (main != this)
            Destroy(this);

        currentOnMinedBuffs = new Dictionary<string, BuffEffectClass>();
        currentOnOpenBuffs = new Dictionary<string, BuffEffectClass>();
        currentCrateBuffs = new Dictionary<string, BuffEffectClass>();
        InitBuffEffects();
    }

    void InitBuffEffects()
    {
        CrateRewardCash crateRewardCash = new CrateRewardCash();
        CrateRewardDamage crateRewardDamage = new CrateRewardDamage();
    }

    //This will Trigger, once the player activates that block in the Inventory or Autosell is enabled.
    public void useBlockBuff(PlayerScript player, int blockId)
    {
        switch(blockId)
        {
            //Crate-F to Crate-S++
            case 55:
                crateEffect(player, 1);
                break;
            case 56:
                crateEffect(player, 2);
                break;
            case 57:
                crateEffect(player, 3);
                break;
            case 58:
                crateEffect(player, 4);
                break;
            case 59:
                crateEffect(player, 5);
                break;
            case 60:
                crateEffect(player, 6);
                break;
            case 61:
                crateEffect(player, 7);
                break;
            case 62:
                crateEffect(player, 8);
                break;
            case 63:
                crateEffect(player, 9);
                break;

        }
    }

    //This will Trigger once the blocks health reaches 0 and gets mined.
    public void OnMinedBlock(PlayerScript player, int id)
    {
        switch(id) {

            //Lava and Magma
            case 16:
            case 17:
                    //W.i.P
                    //TODO: Implement BuffLava
                    ApplyBuffByName(player, currentOnMinedBuffs, "BuffLava", new string[] { "checkIfPlayerHasRadsandStuff" });
            break;
        }
    }

    //Give Buff effect to player
    private void ApplyBuffByName(PlayerScript player, Dictionary<string, BuffEffectClass> dict, string buffEffectName, string[] args)
    {
        if (dict.ContainsKey(buffEffectName))
        {
            dict[buffEffectName].applyBuff(player, args);
        }
    }

    //Crate Script 
    public void crateEffect(PlayerScript player, int strenghtNumber)
    {
        float currentWeight = 0;
        float rng = Random.Range(0, maxCrateWeight);
        
        foreach (var crate in currentCrateBuffs)
        {
            if(rng <= crate.Value.BuffEffectChance + currentWeight)
            {
                crate.Value.applyBuff(player, new string[] { strenghtNumber.ToString() });
                break;
            }
            currentWeight += crate.Value.BuffEffectChance;
        }

        Debug.Log("Mined Crate with " + strenghtNumber);
    }
}
