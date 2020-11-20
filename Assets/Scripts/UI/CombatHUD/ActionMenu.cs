using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenu : MonoBehaviour
{

    public List<ActionButton> buttons;

    public GameObject descriptionBox;
    public GameObject retreatConfirmationPrefab;

    void Start()
    {

        EnableDisplay(false);
    }

    public void DisplaySkillButtons(int turnNum)
    {
        EnableDisplay(true);

        List<string> playerOne = new List<string> { "basicAttack", "strongAttack", "wideAttack"};
        List<string> playerTwo = new List<string> { "basicAttack", "medAttack", "healTarget" };

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
