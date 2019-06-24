using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Console
{
    //Base class for Console commands
    public abstract class ConsoleCommand
    {
        public abstract string name { get; protected set; }
        public abstract string command { get; protected set; }
        public abstract string desc { get; protected set; }

        // Adds that console command to the dictionary
        public void AddCommandToConsole()
        {
            ConsoleSystem.main.AddCommands(command, this);
        }

        // Command exectution
        public abstract void RunCommand(string[] args);
    }


    public class ConsoleSystem : MonoBehaviour
    {
        //Inspector field
        [Header("UI")]
        [SerializeField]
        private Canvas consoleCanvas;

        [SerializeField]
        private Text consoleText;

        [SerializeField]
        private Text inputText;

        [SerializeField]
        private InputField inputField;


        public static ConsoleSystem main { get; private set; }
        public Dictionary<string, ConsoleCommand> availableCommands { get; private set; }

        private void Awake()
        {
            if(main != null)
            {
                Destroy(this);
            }
            main = this;
            availableCommands = new Dictionary<string, ConsoleCommand>();
        }

        // Start is called before the first frame update
        void Start()
        {
            consoleCanvas.gameObject.SetActive(false);
            CreateCommands();
        }

        private void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        void HandleLog(string logMessage, string stackTrace, LogType type)
        {
            string message = "[" + type.ToString() + "] " + logMessage;

            switch (type)
            {
                case LogType.Log:
                    MessageToConsole(message);
                    break;
                case LogType.Warning:
                    WarningMessageToConsole(message);
                    break;
                case LogType.Error:
                case LogType.Exception:
                case LogType.Assert:
                    ErrorMessageToConsole(message);
                    break;
            }
        }

        void CreateCommands()
        {
            CommandSay commandSay = new CommandSay();
            CommandQuit commandQuit = new CommandQuit();
            CommandGive commandGive = new CommandGive();
            CommandBomb commandBomb = new CommandBomb();
            CommandModifier commandModifier = new CommandModifier();
            CommandGiveXP commandGiveXP = new CommandGiveXP();
            CommandGivePerkPoint commandGivePerkPoint = new CommandGivePerkPoint();
            CommandGiveCash commandGiveCash = new CommandGiveCash();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F12))
            {
                if (!Gamemanager.main.player.playerControl)
                    Gamemanager.main.player.playerControl = true;

                consoleCanvas.gameObject.SetActive(!consoleCanvas.gameObject.activeInHierarchy);
            }

            if (consoleCanvas.gameObject.activeInHierarchy)
            {
                inputField.Select();
                inputField.ActivateInputField();

                Gamemanager.main.player.playerControl = false;
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if(inputText.text != "")
                    {
                        MessageToConsole("<color=#747474>" + inputText.text + "</color>");
                        ParseInput(inputText.text);
                        inputField.text = "";
                    }
                }
            }
        }

        public void AddCommands(string name, ConsoleCommand command)
        {
            if (!availableCommands.ContainsKey(name))
            {
                availableCommands.Add(name, command);
            }
        }

        public void MessageToConsole(string msg)
        {
            consoleText.text += msg + "\n";
        }

        public void ErrorMessageToConsole(string msg)
        {
            consoleText.text += "<color=#FF0000>" + msg + "</color>\n";
        }

        public void WarningMessageToConsole(string msg)
        {
            consoleText.text += "<color=#FFE000>" + msg + "</color>\n";
        }

        void ParseInput(string input)
        {
            string[] splitInput = input.Split(" "[0]);

            if(splitInput.Length == 0 || splitInput == null)
            {
                MessageToConsole("Command not recognized.");
                return;
            }

            if (!availableCommands.ContainsKey(splitInput[0]))
            {
                MessageToConsole("Command not recognized.");
            }
            else
            {
                availableCommands[splitInput[0]].RunCommand(splitInput);
            }
        }
    }
}
