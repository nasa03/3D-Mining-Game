using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Stats : MonoBehaviour
{
    public Text statText;
    // Start is called before the first frame update
    void Start()
    {
        statText.text =
            Gamemanager.main.player.stats["damage"].ToString() +
            Gamemanager.main.player.stats["speed"].ToString() +
            Gamemanager.main.player.stats["reach"].ToString() +
            Gamemanager.main.player.stats["critical chance"].ToString() +
            Gamemanager.main.player.stats["critical damage"].ToString() +
            Gamemanager.main.player.stats["luck"].ToString() +
            Gamemanager.main.player.stats["jetpack force"].ToString();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
