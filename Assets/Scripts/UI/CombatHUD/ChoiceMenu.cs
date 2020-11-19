using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceMenu : MonoBehaviour
{

    private EventController controller;

    public Button skills;
    public Button tactics;
    public Button items;


    void Start()
    {
        controller = GameObject.Find("EventController").GetComponent<EventController>();

        skills.onClick.AddListener(() => DisplayMenuType("Skills", controller.turnNum));
        tactics.onClick.AddListener(() => DisplayMenuType("Tactics", controller.turnNum));
        items.onClick.AddListener(() => DisplayMenuType("Items", controller.turnNum));

    }

    private void DisplayMenuType(string type, int turnNum)
    {

        controller.actionMenu.DeselectAllButtons();
        controller.actionMenu.HideButtons();
        Debug.Log(type);
   
        switch (type)
        {
            case "Skills":
                controller.actionMenu.DisplaySkillButtons(turnNum);
                break;
            case "Tactics":
                controller.actionMenu.DisplayTacticButtons();
                break;
            case "Items":
                controller.actionMenu.DisplayItemButtons();
                break;
            default:
                break;
        }
    }

}
