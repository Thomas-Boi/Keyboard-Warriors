using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    private EventController controller;
    public Skill skill;
    public string alias = "";


    void Start()
    {

        controller = GameObject.Find("EventController").GetComponent<EventController>();
        GetComponent<Button>().onClick.AddListener(() => targetSkill());
        select(false);
    }

    public void showTooltip()
    {
        //select(true);
        controller.descriptionBox.text = skill.Description;


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
            controller.deselectAllButtons();
            GetComponent<Image>().color = Color.cyan;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }
    }

    public void spawnButton(Skill skill)
    {
        this.skill = skill;
        GetComponentInChildren<Text>().text = skill.Alias;
        gameObject.SetActive(true);
    }

    private void targetSkill()
    //todo: check skill class to determine targeting class (enemy or ally)
    {
        controller.resetTargetting();
        select(true);
        controller.descriptionBox.text = skill.Description;
        controller.tooltip = skill.Description;
        controller.selectedSkill = skill;

        if (skill.TargetType == TargetType.ENEMY_SINGLE)
        {
            foreach (Character character in controller.getEnemies())
            {
                character.isTargetable = true;
            }
        }
        else if (skill.TargetType == TargetType.ALLY_SINGLE)
        {
            foreach (Character character in controller.getPlayers())
            {
                character.isTargetable = true;
            }
        }

    }
}
