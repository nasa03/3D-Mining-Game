using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Stats : MonoBehaviour
{
    public Text statText;
    PlayerScript localPlayer;
    // Start is called before the first frame update
    void Start()
    {

        localPlayer = Gamemanager.main.getLocalPlayer();

        statText.text =
            localPlayer.stats["damage"].ToString() +
            localPlayer.stats["speed"].ToString() +
            localPlayer.stats["reach"].ToString() +
            localPlayer.stats["critical_chance"].ToString() +
            localPlayer.stats["critical_damage"].ToString() +
            localPlayer.stats["luck"].ToString() +
            localPlayer.stats["jetpack_force"].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetDialogue()
    {
        Gamemanager.main.dialogue.createDialogue("This will RESET your whole progress. Are you sure you wanna do this?", new UnityEngine.Events.UnityAction(reset), null);
    }

    public void reset()
    {
        PlayerPrefs.DeleteAll();
        SaveScript.isReset = true;
        Perks.PerkSystem.activePerks.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
