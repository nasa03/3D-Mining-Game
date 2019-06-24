using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
    public float multiplier = 1;
    public ParticleSystem hitPart;
    public Light flashlight;
    bool isClicking;

    private float _cash;
    public float cash
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
        
        if (PlayerPrefs.HasKey("PlayerCash"))
        {
            cash = PlayerPrefs.GetFloat("PlayerCash");

            applyPlayerStats(PlayerPrefs.GetInt("PlayerDamage"), 
                PlayerPrefs.GetInt("PlayerSpeed"), 
                PlayerPrefs.GetInt("PlayerReach"), 
                PlayerPrefs.GetInt("PlayerCritChance"),
                PlayerPrefs.GetInt("PlayerCritDMG"),
                PlayerPrefs.GetInt("PlayerLuck"),
                PlayerPrefs.GetInt("PlayerJet"));
        }
        else
        {
            applyPlayerStats(1,1,1,1,1,1,1);
            cash = 0;

        }
        speedStore = stats["speed"].finalValue;

        message = GameObject.Find("GameManager").GetComponent<MessageSystem>();
        
        playerControl = true;
        cash = 1000000000000;
        Debug.Log(stats["damage"].finalValue);
    }

    void applyPlayerStats(int damage, int speed, int reach, int critChance, int critDMG, int luck, int jetForce)
    {
        stats.Add("damage", new PlayerStats("Damage", "", 1, 1, damage, 1000, 15, 6));
        stats.Add("speed", new PlayerStats("Speed", "s", 1, -0.005f, speed, 101, 40, 35));
        stats.Add("reach", new PlayerStats("Reach", "b", 1.5f, 0.75f, reach, 100, 20, 15));
        stats.Add("critical chance", new PlayerStats("Critical Chance", "%", 3f, 0.25f, critChance, 20, 35, 12));
        stats.Add("critical damage", new PlayerStats("Critical Damage", "", 1.2f, 0.25f, critDMG, 20, 75, 50));
        stats.Add("luck", new PlayerStats("Luck", "%", 0, 0.75f, luck, 15, 200, 125));
        stats.Add("jetpack force", new PlayerStats("Jetpack Force", "", 0.15f, 0.10f, jetForce, 5, 150, 100));
        //stats["speed"].addModifier(new StatModifier("Test", "%", Operant.percent, "This is a test Modifier", -0.10f, 5));
    }

    void Update()
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

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

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

        if (Input.GetKeyDown(KeyCode.N))
        {
                GameObject bomb = Instantiate(Resources.Load("Prefab/Bomb")) as GameObject;
                bomb.transform.position = transform.position;
                bomb.GetComponent<Explode>().ignoreOres = true;
                bomb.GetComponent<Explode>().radius = 6;
                bomb.GetComponent<Explode>().speed = 0.0001f;
        }

        if (Physics.Raycast(ray, out hit, stats["reach"].finalValue) && playerControl)
        {
            Debug.DrawLine(ray.origin, hit.point);

            if (Input.GetKeyDown(KeyCode.E))
            {

            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Destroy(selectedBlock);
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

        if (Input.GetMouseButton(0))
        {
            if (!isClicking)
            {
                isClicking = true;
            }
            else
            {
                return;
            }

            if (selectedBlock == null)
            {
                return;
            }
            if (selectedBlock.transform.tag == "Block")
            {
                bool isCritHit = CriticalHit(stats["critical chance"].finalValue);
                Instantiate(hitPart, selectedBlock.transform.position, Quaternion.Inverse(transform.GetComponentInChildren<Camera>().transform.rotation));
                Block playerInteractingWithBlock = selectedBlock.GetComponent<Block>();
                playerInteractingWithBlock.hurtBlock(stats["damage"].finalValue * (isCritHit ? stats["critical damage"].finalValue : 1));
            }
        }
        ToggleFlashLight(Input.GetKeyDown(KeyCode.Q));

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
            Debug.Log("Click Crit " + Random.value);
            GameObject.Find("GameCanvas").GetComponent<BlockUiController>().startShaking = true;
            return true;
        }
        return false;
    }

    public void giveCash(double amount)
    {
        cash += Mathf.Floor((float)amount);
        Debug.Log("Gave $" + amount + " to Player!");
    }
}


