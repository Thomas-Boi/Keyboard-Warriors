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
        tactics.onClick.AddListener(() => DisplayMenuType("Retreat"));
        items.onClick.AddListener(() => DisplayMenuType("Items"));

    }

    private void DisplayMenuType(string type, int turnNum = 0)
    {

        controller.actionMenu.DeselectAllButtons();
        controller.actionMenu.HideButtons();
   
        switch (type)
        {
            case "Skills":
                controller.actionMenu.DisplaySkillButtons(turnNum);
                break;
            case "Retreat":
                controller.actionMenu.DisplayRetreatConfirmation();
                break;
            case "Items":
                controller.actionMenu.DisplayItemButtons();
                break;

        }
    }

}
