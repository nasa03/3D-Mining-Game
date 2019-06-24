using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {


    /*
    private float _drillDurability;
    public float drillDurability
    {
        get { return _drillDurability; }
        set { _drillDurability = value; }
    }

    private int _drillLevel;
    public int drillLevel
    {
        get { return _drillLevel; }
        set { _drillLevel = value; }
    } 

    public float addDrillDurability(float value)
    {
        drillDurability += value;
        GameObject.Find("DrillUI").GetComponent<DrillOverwatch>().text.text = drillDurability.ToString();
        return value;
    }  
         */

    MessageSystem message;
    private float speedStore;
    public GameObject selectedBlock, pointer;
    public Highlightblock highlightBlock;
    public BlockUiController blockUI;
    public ParticleSystem hitPart;
    public Light flashlight;
    public Camera camera;
    public XPSystem xp;
    bool isClicking;
    bool autoClickEnabled;

    Ray ray;
    RaycastHit hit;

    private double _cash;
    public double cash
    {
        get { return _cash; }
        set { _cash = value; }
    }

    private bool _playerControl;
    public bool playerControl
    {
        get { return _playerControl; }
        set { _playerControl = value; }
    }

    public Dictionary<string, PlayerStats> stats = new Dictionary<string, PlayerStats>();

    // Use this for initialization
    void Start () {
        //applyPlayerStats(1, 1, 1, 1, 1, 1, 1);
        
        if (!SaveScript.isReset)
        {
            cash = double.Parse(SaveScript.savedData["CurrencyCash"].ToString());
            applyPlayerStats((int)SaveScript.savedData["PlayerDamage"],
                (int)SaveScript.savedData["PlayerSpeed"],
                (int)SaveScript.savedData["PlayerReach"],
                (int)SaveScript.savedData["PlayerCritChance"],
                (int)SaveScript.savedData["PlayerCritDamage"],
                (int)SaveScript.savedData["PlayerLuck"],
                (int)SaveScript.savedData["PlayerJet"]);

            xp = new XPSystem(1,1,15,1.15f);
        }
        else
        {
            applyPlayerStats(1,1,1,1,1,1,1);
            xp = new XPSystem(1, 1, 15, 1.15f);
            cash = 0;

        }
        speedStore = stats["speed"].finalValue;

        //message = GameObject.Find("GameManager").GetComponent<MessageSystem>();
        
        playerControl = true;
        //cash = 1000000000;
    }

    void applyPlayerStats(int damage, int speed, int reach, int critChance, int critDMG, int luck, int jetForce)
    {
        stats.Add("damage", new PlayerStats("Damage", "", 1, 1, damage, 1000, 15, 6));
        stats.Add("speed", new PlayerStats("Speed", "s", 1, -0.01f, speed, 51, 40, 35));
        stats.Add("reach", new PlayerStats("Reach", "b", 1.5f, 0.75f, reach, 100, 20, 15));
        stats.Add("critical_chance", new PlayerStats("Critical Chance", "%", 3f, 0.25f, critChance, 20, 35, 12));
        stats.Add("critical_damage", new PlayerStats("Critical Damage", "", 1.2f, 0.25f, critDMG, 20, 75, 50));
        stats.Add("luck", new PlayerStats("Luck", "%", 0, 0.75f, luck, 15, 200, 125));
        stats.Add("jetpack_force", new PlayerStats("Jetpack Force", "", 0.05f, 0.05f, jetForce, 10, 150, 100));
        //stats["speed"].addModifier(new StatModifier("Test", "%", Operant.add, "This is a test Modifier", -0.8f, 5));
        //stats["damage"].addModifier(new StatModifier("Test", "+", Operant.add, "This is a test Modifier", 1000, 5));
    }

    void Update()
    {
        userInputs();
        onBlockRaycast();
       
    }

    void onBlockRaycast()
    {
        
        ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, stats["reach"].finalValue) && playerControl)
        {
            Debug.DrawLine(ray.origin, hit.point);

            if (Input.GetKeyDown(KeyCode.E))
            {

            }

            if (Input.GetKeyDown(KeyCode.R))
            {

            }

            if (hit.transform.tag == "Block")
            {
                selectedBlock = hit.transform.gameObject;
            }
        }
        else
        {
            selectedBlock = null;
        }
    }

    void userInputs()
    {
        if (isClicking)
        { 
            speedStore -= Time.deltaTime;
            if (speedStore <= 0)
            {
                speedStore = stats["speed"].finalValue;
                isClicking = false;
            }
        }

        if (!isClicking && autoClickEnabled && Gamemanager.main.option_AutoClick)
        {
            hitBlock();
            isClicking = true;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            /*if (drillDurability > 0)
            {
                GameObject drill = Instantiate(Resources.Load("Prefab/Drill")) as GameObject;
                drill.GetComponent<DrillScript>().durability = drillDurability;
                drill.GetComponent<DrillScript>().mineLevel = drillLevel;
                GameObject.FindObjectOfType<DrillOverwatch>().hasDrillPurchased = false;
                addDrillDurability(-drillDurability);
                drill.transform.position = Pointer.transform.position;
                drill.transform.rotation = Pointer.transform.rotation;
            }
            else
            {
                message.PostAnnouncement("You have no drills right now. Buy some in the shop!", 6f);
            } */
        }

        if (Input.GetMouseButtonDown(0) && Gamemanager.main.option_AutoClick)
        {
            autoClickEnabled = !autoClickEnabled;
        }

        if (Input.GetMouseButton(0) && !Gamemanager.main.option_AutoClick)
        {
            if (!isClicking)
            {
                isClicking = true;
                hitBlock();
            }
            else
            {
                return;
            }
        }

        ToggleFlashLight(Input.GetKeyDown(KeyCode.Q));
    }

    public void createBomb()
    {
        GameObject bomb = Instantiate(Resources.Load("Prefab/Bomb")) as GameObject;
        bomb.transform.position = transform.position;
        bomb.GetComponent<Explode>().ignoreOres = false;
        bomb.GetComponent<Explode>().radius = 18;
        bomb.GetComponent<Explode>().speed = 0.00001f;
    }

    void hitBlock()
    {
        if (selectedBlock != null)
        {
            if (selectedBlock.transform.tag == "Block")
            {
                highlightBlock.transform.position = selectedBlock.transform.position;
                highlightBlock.startHighlight();
                bool isCritHit = CriticalHit(stats["critical_chance"].finalValue);

                if(isCritHit)
                {
                    AudioManager.main.Play("Block_Crit", Random.Range(0.75f, 1.5f), highlightBlock.gameObject, false);
                }

                //Instantiate(hitPart, selectedBlock.transform.position, Quaternion.Inverse(transform.GetComponentInChildren<Camera>().transform.rotation));
                selectedBlock.GetComponent<Block>().HurtBlock(stats["damage"].finalValue * (isCritHit ? stats["critical_damage"].finalValue : 1));
            }
        }
    }

    void ToggleFlashLight(bool key)
    {
        if (key)
        {
            flashlight.enabled = !flashlight.enabled;
        }
    }

    bool CriticalHit(float crit)
    {
        if (Random.value < crit / 100)
        {
            blockUI.startShaking = true;
            return true;
        }
        return false;
    }

    public void giveCash(double amount)
    {
        if(amount == 0)
            return;
        
        cash += System.Math.Floor(amount);
        GameUITop.main.UpdateCashUI(System.Math.Abs(amount));
        //Debug.Log("Gave $" + amount + " to Player!");
    }
}


