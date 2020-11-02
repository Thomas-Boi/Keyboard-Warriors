using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceMenu : MonoBehaviour
{

    private EventController controller;
    public ActionMenu actionMenu;

    public Button skills;
    public Button tactics;
    public Button items;


    void Start()
    {
        controller = GameObject.Find("EventController").GetComponent<EventController>();
        //actionMenu = GameObject.Find("ActionMenu").GetComponent<ActionMenu>();

        skills.onClick.AddListener(() => DisplayMenuType("Skills", controller.turnNum));
        tactics.onClick.AddListener(() => DisplayMenuType("Tactics", controller.turnNum));
        items.onClick.AddListener(() => DisplayMenuType("Items", controller.turnNum));

    }

    private void DisplayMenuType(string type, int turnNum)
    {

        actionMenu.DeselectAllButtons();
        actionMenu.HideButtons();
        Debug.Log(type);
   
        switch (type)
        {
            case "Skills":
                actionMenu.DisplaySkillButtons(turnNum);
                break;
            case "Tactics":
                actionMenu.DisplayTacticButtons();
                break;
            case "Items":
                actionMenu.DisplayItemButtons();
                break;
            default:
                break;
        }
    }

}
