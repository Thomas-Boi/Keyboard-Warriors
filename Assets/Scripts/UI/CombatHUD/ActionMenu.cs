﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenu : MonoBehaviour
{
    // repurpose skill buttons for items and tactics later
    public List<ActionButton> buttons;
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

    public void DisplayTacticButtons()
    {

        EnableDisplay(true);

        List<string> tactics = new List<string> { "retreat", "switchRow", "destress" };

        for (int i = 0; i < tactics.Count; i++)
        {
            buttons[i].spawnButton(EventController.tacticsManager.GetTacticByName(tactics[i]));
        }

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

    public void EnableDisplay(bool enabled)
    {
        gameObject.SetActive(enabled);
        descriptionBox.SetActive(enabled);
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
