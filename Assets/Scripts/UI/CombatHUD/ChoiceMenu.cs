using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceMenu : MonoBehaviour
{

    private EventController controller;
    private ActionMenu actionMenu;

    public Button skills;
    public Button tactics;
    public Button items;


    void Start()
    {
        controller = GameObject.Find("EventController").GetComponent<EventController>();
        actionMenu = GameObject.Find("ActionMenu").GetComponent<ActionMenu>();

        skills.onClick.AddListener(() => DisplayMenuType("Skills", controller.turnNum));
        tactics.onClick.AddListener(() => DisplayMenuType("Tactics", controller.turnNum));
        items.onClick.AddListener(() => DisplayMenuType("Items", controller.turnNum));

    }

    private void DisplayMenuType(string type, int turnNum)
    {
        switch (type)
        {
            case "Skills":
                Debug.Log(type);
                actionMenu.HideButtons();
                actionMenu.DisplaySkillButtons(turnNum, controller.GetSkillManager());
                break;
            case "Tactics":
                Debug.Log(type);
                actionMenu.HideButtons();
                actionMenu.DisplayTacticButtons(turnNum);
                break;
            case "Items":
                Debug.Log(type);
                actionMenu.HideButtons();
                actionMenu.DisplayItemButtons(turnNum);
                break;
            default:
                break;
        }
    }

}
