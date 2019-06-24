using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Upgrade : MonoBehaviour
{
    //damage
    [SerializeField]
    GameObject damagePanel;
    [SerializeField]
    Text damageLevel;
    [SerializeField]
    Text damageCost;

    [SerializeField]
    GameObject speedPanel;
    [SerializeField]
    Text speedLevel;
    [SerializeField]
    Text speedCost;

    [SerializeField]
    GameObject reachPanel;
    [SerializeField]
    Text reachLevel;
    [SerializeField]
    Text reachCost;

    [SerializeField]
    GameObject critChancePanel;
    [SerializeField]
    Text critChanceLevel;
    [SerializeField]
    Text critChanceCost;

    [SerializeField]
    GameObject critDamagePanel;
    [SerializeField]
    Text critDamageLevel;
    [SerializeField]
    Text critDamageCost;

    [SerializeField]
    GameObject luckPanel;
    [SerializeField]
    Text luckLevel;
    [SerializeField]
    Text luckCost;

    [SerializeField]
    GameObject jetForcePanel;
    [SerializeField]
    Text jetForceLevel;
    [SerializeField]
    Text jetForceCost;

    HoverBox damageHover, speedHover, reachHover, critChanceHover, critDamageHover, luckHover, jetForceHover;
    
    // Start is called before the first frame update
    void Start()
    {
        damageHover = damagePanel.GetComponent<HoverBox>();
        speedHover = speedPanel.GetComponent<HoverBox>();
        reachHover = reachPanel.GetComponent<HoverBox>();
        critChanceHover = critChancePanel.GetComponent<HoverBox>();
        critDamageHover = critDamagePanel.GetComponent<HoverBox>();
        luckHover = luckPanel.GetComponent<HoverBox>();
        jetForceHover = jetForcePanel.GetComponent<HoverBox>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Gamemanager.main.isTick) {


            damageHover.setDisplay("<b>Strength</b>\nLevel: " + Gamemanager.main.player.stats["damage"].level + "\nCost: $ " + Gamemanager.main.player.stats["damage"].finalCost + "\n\nIncreases your mining damage. ");
            damageLevel.text = Gamemanager.main.player.stats["damage"].finalValue.ToString();
            damageCost.text = "$" + Gamemanager.main.player.stats["damage"].finalCost.ToString();
            if(Gamemanager.main.player.cash < Gamemanager.main.player.stats["damage"].finalCost)
            {
                damagePanel.GetComponent<Button>().interactable = false;
            }
            else
            {
                damagePanel.GetComponent<Button>().interactable = true;
            }

            speedHover.setDisplay("<b>Speed</b>\nLevel: " + Gamemanager.main.player.stats["speed"].level + "\nCost: $" + Gamemanager.main.player.stats["speed"].finalCost + "\n\nDecreases the time it takes to swing.");
            speedLevel.text = Gamemanager.main.player.stats["speed"].finalValue.ToString() + "s";
            speedCost.text = "$" + Gamemanager.main.player.stats["speed"].finalCost.ToString();
            if (Gamemanager.main.player.cash < Gamemanager.main.player.stats["speed"].finalCost)
            {
                speedPanel.GetComponent<Button>().interactable = false;
            }
            else
            {
                speedPanel.GetComponent<Button>().interactable = true;
            }

            reachHover.setDisplay("<b>Reach</b>\nLevel: " + Gamemanager.main.player.stats["reach"].level + "\nCost: $" + Gamemanager.main.player.stats["reach"].finalCost + "\n\nIncreases the range.");
            reachLevel.text = Gamemanager.main.player.stats["reach"].finalValue.ToString() + "blocks";
            reachCost.text = "$" + Gamemanager.main.player.stats["reach"].finalCost.ToString();
            if (Gamemanager.main.player.cash < Gamemanager.main.player.stats["reach"].finalCost)
            {
                reachPanel.GetComponent<Button>().interactable = false;
            }
            else
            {
                reachPanel.GetComponent<Button>().interactable = true;
            }

            critChanceHover.setDisplay("<b>Critical Chance</b>\nLevel: " + Gamemanager.main.player.stats["critical_chance"].level + "\nCost: $" + Gamemanager.main.player.stats["critical_chance"].finalCost + "\n\nIncreases the chance of a critical hit.");
            critChanceLevel.text = Gamemanager.main.player.stats["critical_chance"].finalValue.ToString() + "%";
            critChanceCost.text = "$" + Gamemanager.main.player.stats["critical_chance"].finalCost.ToString();
            if (Gamemanager.main.player.cash < Gamemanager.main.player.stats["critical_chance"].finalCost)
            {
                critChancePanel.GetComponent<Button>().interactable = false;
            }
            else
            {
                critChancePanel.GetComponent<Button>().interactable = true;
            }

            critDamageHover.setDisplay("<b>Critical Damage</b>\nLevel: " + Gamemanager.main.player.stats["critical_damage"].level + "\nCost: $" + Gamemanager.main.player.stats["critical_damage"].finalCost + "\n\nIncreases the damage of a critical hit.");
            critDamageLevel.text = Gamemanager.main.player.stats["critical_damage"].finalValue.ToString() + "%";
            critDamageCost.text = "$" + Gamemanager.main.player.stats["critical_damage"].finalCost.ToString();
            if (Gamemanager.main.player.cash < Gamemanager.main.player.stats["critical_damage"].finalCost)
            {
                critDamagePanel.GetComponent<Button>().interactable = false;
            }
            else
            {
                critDamagePanel.GetComponent<Button>().interactable = true;
            }

            luckHover.setDisplay("<b>Luck</b>\nLevel: " + Gamemanager.main.player.stats["luck"].level + "\nCost: $" + Gamemanager.main.player.stats["luck"].finalCost + "\n\nIncreases the luck for finding ores/blocks and other things.");
            luckLevel.text = Gamemanager.main.player.stats["luck"].finalValue.ToString() + "%";
            luckCost.text = "$" + Gamemanager.main.player.stats["luck"].finalCost.ToString();
            if (Gamemanager.main.player.cash < Gamemanager.main.player.stats["luck"].finalCost)
            {
                luckPanel.GetComponent<Button>().interactable = false;
            }
            else
            {
                luckPanel.GetComponent<Button>().interactable = true;
            }

            jetForceHover.setDisplay("<b>Jetpack Force</b>\nLevel: " + Gamemanager.main.player.stats["jetpack_force"].level + "\nCost: $" + Gamemanager.main.player.stats["jetpack_force"].finalCost + "\n\nIncreases the force of your jetpack.");
            jetForceLevel.text = Gamemanager.main.player.stats["jetpack_force"].finalValue.ToString() + "";
            jetForceCost.text = "$" + Gamemanager.main.player.stats["jetpack_force"].finalCost.ToString();
            if (Gamemanager.main.player.cash < Gamemanager.main.player.stats["jetpack_force"].finalCost)
            {
                jetForcePanel.GetComponent<Button>().interactable = false;
            }
            else
            {
                jetForcePanel.GetComponent<Button>().interactable = true;
            }

        }
    }

    public void buyUpgrade(string type) {
        switch (type)
        {
            case "damage":
                Gamemanager.main.player.stats["damage"].BuyLevelUp(1);
                break;

            case "speed":
               
                Gamemanager.main.player.stats["speed"].BuyLevelUp(1);
                break;

            case "reach":

                Gamemanager.main.player.stats["reach"].BuyLevelUp(1);
                break;

            case "critChance":

                Gamemanager.main.player.stats["critical_chance"].BuyLevelUp(1);
                break;

            case "critDamage":

                Gamemanager.main.player.stats["critical_damage"].BuyLevelUp(1);
                break;

            case "luck":

                Gamemanager.main.player.stats["luck"].BuyLevelUp(1);
                break;

            case "jetForce":

                Gamemanager.main.player.stats["jetpack_force"].BuyLevelUp(1);
                break;
            default:

            break;
        }
    }
}
