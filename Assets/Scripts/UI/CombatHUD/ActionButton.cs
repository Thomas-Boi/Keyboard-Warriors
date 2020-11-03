using System.Collections;
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

        if (action.TargetType == TargetType.ENEMY_SINGLE)
        {
            foreach (Character character in controller.getEnemies())
            {
                character.isTargetable = true;
            }
        }
        else if (action.TargetType == TargetType.ALLY_SINGLE)
        {
            foreach (Character character in controller.getPlayers())
            {
                character.isTargetable = true;
            }
        } else if (action.TargetType == TargetType.SELF)
        {
            Character character = controller.getPlayers()[controller.turnNum];
            character.isTargetable = true;
        }

    }
}
