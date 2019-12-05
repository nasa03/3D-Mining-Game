using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Perks { 
    public abstract class Perk
    {
        public abstract string id { get; protected set; }
        public abstract string name { get; protected set; }
        public abstract string desc { get; protected set; }
        public abstract int level { get; protected set; }
        public abstract int maxLevel { get; protected set; }
        public abstract int cost { get; protected set; }
        public abstract bool isActive { get; protected set; }
        private GameObject panel;

        protected abstract void ActivatePerk(PlayerScript player, int amount);

        public void LevelUp(PlayerScript player, int amount)
        {
            if (maxLevel <= level)
                level = maxLevel;
            else
                level += amount;

            if (level >= 1) { 
                isActive = true;
                if(!PerkSystem.activePerks.Contains(this))
                    PerkSystem.activePerks.Add(this);
            }
            ActivatePerk(player, amount);
        }

        public void BuyPerk(PlayerScript player, int amount)
        {
            if (player.xp.perkpoint >= cost) { 
                player.xp.GivePerkPoint(-cost * amount);
                LevelUp(player, amount);
            }
            panel.GetComponent<HoverBox>().setDisplay(ToString());
        }

        public void ConfirmBuyPerk(PlayerScript player, GameObject panel)
        {
            this.panel = panel;
            if (player.xp.perkpoint >= cost)
            {
                Gamemanager.main.dialogue.createDialogue("Are you sure you wanna buy " + name + "?", () => BuyPerk(player, 1), null);
            }
        }

        public void AddToList()
        {
            PerkSystem.currentPerks.Add(id, this);
        }

        override
        public string ToString()
        {
            return "<b>" + name + "</b>\n\n" + desc + "\n\nCost: " + cost + "\n\nLevel: " + level + "/" + maxLevel;
        }
    }
    #region Perklist
    public class DamagePerk : Perk
    {
        public override string id { get; protected set; }
        public override string name { get; protected set; }
        public override string desc { get; protected set; }
        public override int level { get; protected set; }
        public override int maxLevel { get; protected set; }
        public override int cost { get; protected set; }
        public override bool isActive { get; protected set; }

        public DamagePerk()
        {
            id = "perk_damage";
            name = "Damage Boost";
            desc = "Increases Damage by +1% per player level. Each stack increases its percentage by +0.5%.";
            maxLevel = 100;
            cost = 1;

            AddToList();
        }

        protected override void ActivatePerk(PlayerScript player, int amount)
        {
            ModifierSystem.ApplyModifier(player, "damage", "perk_damage", amount);
        }
    }

    public class SpeedPerk : Perk
    {
        public override string id { get; protected set; }
        public override string name { get; protected set; }
        public override string desc { get; protected set; }
        public override int level { get; protected set; }
        public override int maxLevel { get; protected set; }
        public override int cost { get; protected set; }
        public override bool isActive { get; protected set; }

        public SpeedPerk()
        {
            id = "perk_speed";
            name = "Speed Boost";
            desc = "Decreases Speed by -1%. 2nd Level reduces it further by -0,5%";
            maxLevel = 2;
            cost = 5;

            AddToList();
        }

        protected override void ActivatePerk(PlayerScript player, int amount)
        {
            ModifierSystem.ApplyModifier(player, "speed", "perk_speed", amount);
        }
    }
    #endregion


    public class PerkSystem : MonoBehaviour
    {
        public static Dictionary<string, Perk> currentPerks { get; private set; }
        public static List<Perk> activePerks = new List<Perk>();

        private void Start()
        {
            InitPerks();
            SaveScript.LoadPerks();
        }

        public void InitPerks()
        {
            currentPerks = new Dictionary<string, Perk>();

            DamagePerk damagePerk = new DamagePerk();
            SpeedPerk speedPerk = new SpeedPerk();
        }

        public static void AssignPerk(PlayerScript player, string id, int level)
        {
            currentPerks[id].LevelUp(player, level);
        }
    }
}