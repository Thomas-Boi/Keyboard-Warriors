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
        
    }

    void OnMouseEnter()
    {

        
        

    }




    void Awake()
    {
        gameObject.SetActive(false);
    }

    public void spawnButton(Skill skill)
    {
        this.skill = skill;
        GetComponentInChildren<Text>().text = skill.alias;
        gameObject.SetActive(true);
    }

    private void targetSkill()
    //todo: check skill class to determine targeting class (enemy or ally)
    {
        controller.selectedSkill = skill.name;

        foreach (Spawner spawn in controller.spawners)
        {
            if (spawn.isOccupied)
            {
                spawn.enemy.isTargetable = true;
            }
        }
    }
}
