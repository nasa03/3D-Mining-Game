using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Perks;
using System.Linq;
using UnityEngine.UI;

public class UI_Perks : MonoBehaviour
{
    public GameObject perkPrefab;
    public Text totalPerkPoints;
    //Cached
    RectTransform perkPanelTransform;
    HoverBox hoverbox;
    Button perkButton;

    int posX = -180;
    int posY = -70;

    // Start is called before the first frame update
    void Start()
    {
        InitPerks();
    }

    private void Update()
    {
        totalPerkPoints.text = "Perk Points: " + Gamemanager.main.player.xp.perkpoint;
    }

    void InitPerks()
    {
        for(int i = 0; i<PerkSystem.currentPerks.Count; i++)
        {
            GameObject perkPanel = new GameObject();
            Perk perk = PerkSystem.currentPerks.ElementAt(i).Value;
            perkPanel = Instantiate(perkPrefab, transform.position, Quaternion.identity);
            perkPanel.transform.SetParent(gameObject.transform);

            perkPanel.GetComponent<Button>().onClick.AddListener(() => perk.ConfirmBuyPerk(perkPanel));
            perkPanel.name = perk.id;

            perkPanelTransform = perkPanel.GetComponent<RectTransform>();
            perkPanelTransform.anchoredPosition = new Vector2(posX + ((perkPanelTransform.sizeDelta.x - 5) * i), posY);

            hoverbox = perkPanel.GetComponent<HoverBox>();
            hoverbox.setDisplay(PerkSystem.currentPerks.ElementAt(i).Value.ToString());
        }
    }
}
