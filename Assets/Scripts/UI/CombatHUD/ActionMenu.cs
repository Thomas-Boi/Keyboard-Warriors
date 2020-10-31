using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenu : MonoBehaviour
{
    // repurpose skill buttons for items and tactics later
    public List<SkillButton> buttons;

    public GameObject descriptionBox;

    void Start()
    {
        EnableDisplay(false);
    }

    public void DisplaySkillButtons(int turnNum)
    {

        EnableDisplay(true);

        List<string> playerOne = new List<string> { "basicAttack", "strongAttack" };
        List<string> playerTwo = new List<string> { "basicAttack", "strongAttack", "healTarget" };

        if (turnNum == 0)
        {
            for (int i = 0; i < playerOne.Count; i++)
            {
                buttons[i].spawnButton(EventController.skillManager.getSkillByName(playerOne[i]));
            }
        }
        else
        {
            for (int i = 0; i < playerTwo.Count; i++)
            {
                buttons[i].spawnButton(EventController.skillManager.getSkillByName(playerTwo[i]));
            }
        }
    }

    public void DisplayTacticButtons(int turnNum)
    {
        // todo: tactic buttons

        List<string> tactics = new List<string> { "retreat", "switch", "deStress" };

        for (int i = 0; i < tactics.Count; i++)
        {
            //buttons[i].spawnButton(EventController.skillManager.getSkillByName(tactics[i]));
        }

        EnableDisplay(true);
    }

    public void DisplayItemButtons(int turnNum)
    {

        List<string> items = new List<string> { "" };

        // todo: item buttons
        EnableDisplay(true);
    }

    public void EnableDisplay(bool enabled)
    {
        gameObject.SetActive(enabled);
        descriptionBox.SetActive(enabled);
    }

    public void DeselectAllButtons()
    {
        foreach (SkillButton button in buttons)
        {
            button.select(false);
        }
    }

    public void HideButtons()
    {
        foreach (SkillButton button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

}
