using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TacticsManager : ActionManager
{

    // todo: tactics from json
    public TextAsset skillJson;
    public SkillDatabase skillData;

    void Start()
    {
        skillData = JsonUtility.FromJson<SkillDatabase>(skillJson.text);
        initController();
    }

    public Skill GetTacticByName(string name)
    {
        SkillDetail details = skillData.skills.Find(x => x.name == name);

        return new Skill(details.name, details.description,
            details.targetType, details.alias, details.conditions);
    }

}
