﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    private EventController controller;
    public Action action;
    public string alias = "";


    void Start()
    {
        controller = GameObject.Find("EventController").GetComponent<EventController>();
        GetComponent<Button>().onClick.AddListener(() => targetAction());
        select(false);
    }

    public void showTooltip()
    {
        //select(true);
        controller.descriptionBox.text = action.Description;
    }

    public void revertTooltip()
    {
        controller.revertDescription();
    }



    void Awake()
    {
        gameObject.SetActive(false);
    }

    public void select(bool selected)
    {
        if (selected == true)
        {
            controller.actionMenu.DeselectAllButtons();
            GetComponent<Image>().color = Color.cyan;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }
    }

    public void spawnButton(Action action)
    {
        this.action = action;
        GetComponentInChildren<Text>().text = action.Alias;
        gameObject.SetActive(true);
    }

    private void targetAction()
    //todo: check skill class to determine targeting class (enemy or ally)
    {
        controller.resetTargetting();
        select(true);
        controller.descriptionBox.text = action.Description;
        controller.tooltip = action.Description;
        controller.selectedAction = action;

        switch (action.TargetType)
        {
            case TargetType.ENEMY_SINGLE:
                foreach (Character enemy in controller.getEnemies())
                {
                    enemy.isTargetable = true;
                }
                break;
            case TargetType.ENEMY_ALL:
                foreach (Character enemy in controller.getEnemies())
                {
                    enemy.isTargetable = true;
                }
                break;
            case TargetType.ALLY_SINGLE:
                foreach (Character player in controller.GetAlivePlayers())
                {
                    player.isTargetable = true;
                }
                break;
            case TargetType.ALLY_ALL:
                foreach (Character player in controller.GetAlivePlayers())
                {
                    player.isTargetable = true;
                }
                break;
            case TargetType.ALLY_SINGLE_DEAD:
                foreach (Character player in controller.GetDeadPlayers())
                {
                    player.isTargetable = true;
                }
                break;
            case TargetType.SELF:
                Character character = controller.GetAlivePlayers()[controller.turnNum];
                character.isTargetable = true;
                break;
        }

        foreach (Character enemy in controller.getEnemies())
        {
            if (!enemy.isTargetable)
            {
                EventController.SetLayerRecursively(enemy.gameObject, 8);

            }
            else {
                EventController.SetLayerRecursively(enemy.gameObject, 0);
            }
        }
        foreach (Character player in controller.players)
        {
            if (!player.isTargetable)
            {
                EventController.SetLayerRecursively(player.gameObject, 8);

            }
            else {
                EventController.SetLayerRecursively(player.gameObject, 0);
            }
        }

    }
}
