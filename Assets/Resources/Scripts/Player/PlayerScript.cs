using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class PlayerScript : MonoBehaviour {

    public double cash { get; set; }
    public bool playerControl { get; set; }
    public int playerId { get; set; }

    public XPSystem xp;

    public GameObject selectedBlock, pointer;
    [SerializeField]
    Highlightblock highlightBlock;
    [SerializeField]
    BlockUiController blockUI;
    [SerializeField]
    ParticleSystem hitPart;
    [SerializeField]
    Light flashlight;
    [SerializeField]
    Camera camera;
    
    bool isClicking;
    bool autoClickEnabled;

    private float speedStore;

    Ray ray;
    RaycastHit hit;

    public Dictionary<string, PlayerStats> stats = new Dictionary<string, PlayerStats>();

    // Use this for initialization
    void Start () {
        //applyPlayerStats(1, 1, 1, 1, 1, 1, 1);

        initPlayer();

        if (!SaveScript.isReset)
        {
            JsonData savedData = Gamemanager.main.getSaveScript().getSaveData();

            cash = double.Parse(savedData["CurrencyCash"].ToString());
            ApplyPlayerStats((int)savedData["PlayerDamage"],
                (int)savedData["PlayerSpeed"],
                (int)savedData["PlayerReach"],
                (int)savedData["PlayerCritChance"],
                (int)savedData["PlayerCritDamage"],
                (int)savedData["PlayerLuck"],
                (int)savedData["PlayerJet"]);

            xp = new XPSystem(1,1,15,1.15f);
        }
        else
        {
            ApplyPlayerStats(1,1,1,1,1,1,1);
            xp = new XPSystem(1, 1, 15, 1.15f);
            cash = 0;

        }
        speedStore = stats["speed"].finalValue;

        //message = GameObject.Find("GameManager").GetComponent<MessageSystem>();
        
        playerControl = true;
        //cash = 1000000000;
    }

    private void initPlayer()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerId = Gamemanager.PlayerAmount;
    }

    void ApplyPlayerStats(int damage, int speed, int reach, int critChance, int critDMG, int luck, int jetForce)
    {
        stats.Add("damage", new PlayerStats("Damage", "", 1, 1, damage, 1000, 15, 6));
        stats.Add("speed", new PlayerStats("Speed", "s", 1, -0.01f, speed, 51, 40, 35));
        stats.Add("reach", new PlayerStats("Reach", "b", 1.5f, 0.025f, reach, 100, 20, 15));
        stats.Add("critical_chance", new PlayerStats("Critical Chance", "%", 3f, 0.25f, critChance, 20, 35, 12));
        stats.Add("critical_damage", new PlayerStats("Critical Damage", "", 1.2f, 0.25f, critDMG, 20, 75, 50));
        stats.Add("luck", new PlayerStats("Luck", "%", 0, 0.75f, luck, 15, 200, 125));
        stats.Add("jetpack_force", new PlayerStats("Jetpack Force", "", 0.05f, 0.05f, jetForce, 10, 150, 100));
        //stats["speed"].addModifier(new StatModifier("Test", "%", Operant.add, "This is a test Modifier", -0.8f, 5));
        //stats["damage"].addModifier(new StatModifier("Test", "+", Operant.add, "This is a test Modifier", 1000, 5));
    }

    void Update()
    {
        UserInputs();
        OnBlockRaycast();
    }

    void OnBlockRaycast()
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

    void UserInputs()
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
            HitBlock();
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
                HitBlock();
            }
            else
            {
                return;
            }
        }

        ToggleFlashLight(Input.GetKeyDown(KeyCode.Q));
    }

    public void CreateBomb()
    {
        GameObject bomb = Instantiate(Resources.Load("Prefab/Bomb")) as GameObject;
        bomb.transform.position = transform.position;
        bomb.GetComponent<Explode>().ignoreOres = false;
        bomb.GetComponent<Explode>().radius = 18;
        bomb.GetComponent<Explode>().speed = 0.00001f;
    }

    void HitBlock()
    {
        if (selectedBlock != null)
        {
            if (selectedBlock.transform.tag == "Block")
            {

                if (highlightBlock != null)
                {
                    highlightBlock.transform.position = selectedBlock.transform.position;
                    highlightBlock.StartHighlight();
                }

                bool isCritHit = CriticalHit(stats["critical_chance"].finalValue);

                if(isCritHit)
                {
                    AudioManager.main.Play("Block_Crit", Random.Range(0.75f, 1.5f), highlightBlock.gameObject, false);
                }

                //Instantiate(hitPart, selectedBlock.transform.position, Quaternion.Inverse(transform.GetComponentInChildren<Camera>().transform.rotation));
                selectedBlock.GetComponent<Block>().HurtBlock(this, stats["damage"].finalValue * (isCritHit ? stats["critical_damage"].finalValue : 1));
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

    public void GiveCash(double amount)
    {
        if(amount == 0)
            return;
        
        cash += System.Math.Floor(amount);
        GameUITop.main.UpdateCashUI(System.Math.Abs(amount));
        //Debug.Log("Gave $" + amount + " to Player!");
    }
}


