using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuffEffect {

    //Every Crate Reward goes here:

    //Give the Player Cash
    public class CrateRewardCash : BuffEffectClass
    {
        public override string BuffEffectName { get; protected set; }
        public override float BuffEffectChance { get; protected set; }
        public override bool IgnoreLuck { get; protected set; }
        public override BuffTypeEnum BuffType { get; protected set; }

        public CrateRewardCash()
        {
            BuffEffectName = "Cash Reward";
            BuffEffectChance = 50f;
            BuffType = BuffTypeEnum.Crate;
            IgnoreLuck = true;

            AddToList();
        }

        public override void applyBuff(PlayerScript player, string[] args)
        {
            double cash = Random.Range(30 , 250) * int.Parse(args[0]);

            player.GiveCash(cash);
            Debug.Log("Got " + cash + " from Crate");
        }
    }

    //Give the Player a levelup to damage
    public class CrateRewardDamage : BuffEffectClass
    {
        public override string BuffEffectName { get; protected set; }
        public override float BuffEffectChance { get; protected set; }
        public override bool IgnoreLuck { get; protected set; }
        public override BuffTypeEnum BuffType { get; protected set; }

        public CrateRewardDamage()
        {
            BuffEffectName = "Damage Reward";
            BuffEffectChance = 25f;
            BuffType = BuffTypeEnum.Crate;
            IgnoreLuck = true;

            AddToList();
        }

        public override void applyBuff(PlayerScript player, string[] args)
        {
            player.stats["damage"].LevelUp(int.Parse(args[0]));
        }
    }

    //TODO:XP Gain

    //Give the Player a higher tier crate
    public class CrateRewardCrate : BuffEffectClass
    {
        public override string BuffEffectName { get; protected set; }
        public override float BuffEffectChance { get; protected set; }
        public override bool IgnoreLuck { get; protected set; }
        public override BuffTypeEnum BuffType { get; protected set; }

        public CrateRewardCrate()
        {
            BuffEffectName = "Crate Reward";
            BuffEffectChance = 2f;
            BuffType = BuffTypeEnum.Crate;
            IgnoreLuck = true;

            AddToList();
        }

        public override void applyBuff(PlayerScript player, string[] args)
        {
            switch (int.Parse(args[0]))
            {
                case 1:

                    break;
            }
        }
    }

    //TODO: Implement XP Gain, Crate Gain, Block Gain, Level Gain, Bomb, Nothing, Heatsuit, Radsuit, Insurance, SellPrice Increase
}
