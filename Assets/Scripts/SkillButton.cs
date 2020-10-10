using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    private EventController controller;
    public string skill = "";
    public string alias = "";




    void Start()
    {

        controller = GameObject.Find("EventController").GetComponent<EventController>();
        GetComponent<Button>().onClick.AddListener(() => targetSkill());
        gameObject.SetActive(false);
    }


    public void spawnButton(string skill, string alias)
    {
        this.skill = skill;
        this.alias = alias;
        GetComponentInChildren<Text>().text = alias;
        gameObject.SetActive(true);
    }

    private void targetSkill()
    //todo: check skill class to determine targeting class (enemy or ally)
    {
        UnityEngine.Debug.Log("Attack");
        controller.selectedSkill = skill;

        foreach (Spawner spawn in controller.spawners)
        {
            if (spawn.isOccupied)
            {
                spawn.enemy.isTargetable = true;
            }
        }
    }
}
