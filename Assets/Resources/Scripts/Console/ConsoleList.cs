using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A list of console commands. For easy management, create your console commands here:
namespace Console
{
    public class CommandSay : ConsoleCommand
    {
        public override string name { get; protected set; }
        public override string command { get; protected set; }
        public override string desc { get; protected set; }

        public CommandSay()
        {
            name = "Say";
            command = "say";
            desc = "Outputs messages.";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            if (args.Length <= 1)
            {
                ConsoleSystem.main.ErrorMessageToConsole("1 Argument Required!");
                return;
            }

            string message = "";
            for (int i = 1; i < args.Length; i++)
            {
                message += args[i] + " ";
            }
            ConsoleSystem.main.MessageToConsole(message);
        }
    }
    public class CommandQuit : ConsoleCommand
    {
        public override string name { get; protected set; }
        public override string command { get; protected set; }
        public override string desc { get; protected set; }

        public CommandQuit()
        {
            name = "Quit";
            command = "quit";
            desc = "Shuts off the game.";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            Application.Quit();
        }
    }
    public class CommandGive : ConsoleCommand
    {
        public override string name { get; protected set; }
        public override string command { get; protected set; }
        public override string desc { get; protected set; }

        public CommandGive()
        {
            name = "Give";
            command = "give";
            desc = "Gives you items based. 1st Argument for type. 2nd Argument for id. 3rd for amount";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            Inventory.main.AddInventory(args[1], double.Parse(args[2]), ItemType.Block);
        }
    }
    public class CommandBomb : ConsoleCommand
    {
        public override string name { get; protected set; }
        public override string command { get; protected set; }
        public override string desc { get; protected set; }

        public CommandBomb()
        {
            name = "Bomb";
            command = "bomb";
            desc = "Creates a Bomb";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            Gamemanager.main.getLocalPlayer().CreateBomb();
        }
    }
    public class CommandModifier : ConsoleCommand
    {
        public override string name { get; protected set; }
        public override string command { get; protected set; }
        public override string desc { get; protected set; }

        public CommandModifier()
        {
            name = "Modifier";
            command = "modifier";
            desc = "Sets a modifier to a stat. Usage: <arg1> = stat, <arg2> = modifiername.";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            ModifierSystem.ApplyModifier(Gamemanager.main.getPlayerByID(int.Parse(args[1])), args[2], args[3], int.Parse(args[4]));
        }
    }
    public class CommandGiveXP : ConsoleCommand
    {
        public override string name { get; protected set; }
        public override string command { get; protected set; }
        public override string desc { get; protected set; }

        public CommandGiveXP()
        {
            name = "GiveXP";
            command = "givexp";
            desc = "Gives the player xp";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            Gamemanager.main.getPlayerByID(int.Parse(args[1])).xp.GiveXP(int.Parse(args[2]));
        }
    }
    public class CommandGivePerkPoint : ConsoleCommand
    {
        public override string name { get; protected set; }
        public override string command { get; protected set; }
        public override string desc { get; protected set; }

        public CommandGivePerkPoint()
        {
            name = "GivePerkPoint";
            command = "giveperkpoint";
            desc = "Gives the player perk points";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            Gamemanager.main.getPlayerByID(int.Parse(args[1])).xp.GivePerkPoint(int.Parse(args[2]));
        }
    }
    public class CommandGiveCash : ConsoleCommand
    {
        public override string name { get; protected set; }
        public override string command { get; protected set; }
        public override string desc { get; protected set; }

        public CommandGiveCash()
        {
            name = "GiveCash";
            command = "givecash";
            desc = "Gives the player cash";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            Gamemanager.main.getPlayerByID(int.Parse(args[1])).GiveCash(int.Parse(args[2]));
        }
    }
}
