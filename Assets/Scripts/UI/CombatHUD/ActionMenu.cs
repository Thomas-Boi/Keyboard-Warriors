using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenu : MonoBehaviour
{
    // repurpose skill buttons for items and tactics later
    public List<SkillButton> buttons;



    public void DisplaySkillButtons(int turnNum, SkillManager skillManager)
    {
        List<string> playerOne = new List<string> { "basicAttack", "strongAttack" };
        List<string> playerTwo = new List<string> { "basicAttack", "strongAttack", "healTarget" };

        if (turnNum == 0)
        {
            for (int i = 0; i < playerOne.Count; i++)
            {
                buttons[i].spawnButton(skillManager.getSkillByName(playerOne[i]));
            }
        }
        else
        {
            for (int i = 0; i < playerTwo.Count; i++)
            {
                buttons[i].spawnButton(skillManager.getSkillByName(playerTwo[i]));
            }
        }
    }

    public void DisplayTacticButtons(int turnNum)
    {
        // todo: tactic buttons
    }

    public void DisplayItemButtons(int turnNum)
    {
        // todo: item buttons
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
