using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenu : MonoBehaviour
{

    public List<ActionButton> buttons;

    public GameObject descriptionBox;
    public GameObject retreatConfirmationPrefab;
    public EventController controller;

    void Start()
    {
        EnableDisplay(false);
        controller = GameObject.Find("EventController").GetComponent<EventController>();
    }

    public void DisplaySkillButtons(int turnNum)
    {
        //todo: Store skills as arrays on characters instead of doing this
        EnableDisplay(true);
        /* 
                List<string> playerOne = new List<string> { "basicAttack", "strongAttack", "wideAttack", "selfBuffAtk"};
                List<string> playerTwo = new List<string> { "basicAttack", "weakWideAttack", "weakHealStress","healStress", "teamHealStress"};
                List<string> playerThree = new List<string> { "basicAttack", "medAttack", "healTarget", "buffAtk" };
         */

        List<string> skills = new List<string> { "basicAttack" };
        if (turnNum < controller.players.Count)
        {
            int level = controller.players[turnNum].level;

            if (turnNum == 0)
            {
                if (level >= 2)
                {
                    skills.Add("strongAttack");
                }
                if (level >= 4)
                {
                    skills.Add("wideAttack");
                }
                if (level >= 5)
                {
                    skills.Add("selfBuffAtk");
                }
                if (level >= 8)
                {
                    skills.Add("strongWideAttack");
                }

            }
            else if (turnNum == 1)
            {
                if (level >= 2)
                {
                    skills.Add("weakHealStress");
                }
                if (level >= 4) {
                    skills.Add("weakWideAttack");
                }
                if (level >= 5) {
                    skills.Add("healStress");
                }
                if (level >= 7) {
                    skills.Add("teamHealStress");
                }
            }
            else
            {
                if (level >= 2)
                {
                    skills.Add("healTarget");
                }
                if (level >= 4)
                {
                    skills.Add("medAttack");
                }
                if (level >= 6)
                {
                    skills.Add("buffAtk");
                }
                if (level >= 8)
                {
                    skills.Add("healTeam");
                }
            }

            for (int i = 0; i < skills.Count; i++)
            {
                buttons[i].spawnButton(EventController.skillManager.getSkillByName(skills[i]));
            }
        }

    }

    public void DisplayRetreatConfirmation()
    {

        EnableDisplay(false, true);

    }

    public void DisplayItemButtons()
    {

        EnableDisplay(true);

        List<Item> items = EventController.ItemManager.GetItems();

        for (int i = 0; i < items.Count; i++)
        {
            buttons[i].spawnButton(items[i]);
        }

    }

    public void EnableDisplay(bool enabled, bool retEnabled = false)
    {
        gameObject.SetActive(enabled);
        descriptionBox.SetActive(enabled);
        retreatConfirmationPrefab.SetActive(retEnabled);
    }

    public void DeselectAllButtons()
    {
        foreach (ActionButton button in buttons)
        {
            button.select(false);
        }
    }

    public void HideButtons()
    {
        foreach (ActionButton button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

}
