using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUITop : MonoBehaviour
{
    public static GameUITop main;
    public TopTextUI moneyUI, xpUI;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        main = this;
        anim = GetComponent<Animator>();
    }

    // Messages Cash Top Text to update its values.
    public void UpdateCashUI(double amount)
    {
        moneyUI.enableTextUI(amount);
        ShowTopUI();
    }

    // Messages XP Top Text to update its values.
    public void UpdateXpUI(float amount)
    {
        xpUI.enableTextUI(amount);
        ShowTopUI();
    }

    // Show the Top UI
    private void ShowTopUI()
    {
        if (!anim.GetBool("showUI") && !Gamemanager.main.option_ShowTopUI)
        {
            anim.SetBool("showUI", true);
        }

        StopCoroutine("HideUI");
        StartCoroutine("HideUI");
    }

    // Toggle for the Top UI to show up permamentally
    public void ToggleTopUI(bool enable)
    {
        anim.SetBool("showUI", enable);
    }

    IEnumerator HideUI()
    {
        yield return new WaitForSeconds(3);
        if (!Gamemanager.main.option_ShowTopUI)
        {
            anim.SetBool("showUI", false);
        }
    }
}
