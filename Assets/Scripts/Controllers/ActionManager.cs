﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// manages the ActionMenu for a specific type of Action (Skills, Tactics, or Items) 
public class ActionManager : MonoBehaviour
{
    protected EventController controller;

    protected void initController()
    {
        controller = GetComponent<EventController>();
    }

    public IEnumerator useAction(Character user, Character target, Action action)
    {
        controller.resetTargetting();
        controller.actionMenu.HideButtons();
        controller.clearDescription();

        user.resetScale();

        controller.DisplaySkillDialogue(user, action.Alias, 1.0f);
        if (controller.playerTurn)
        {
            user.SetStatus("atkUp", user.atkUp - 1);
            user.SetStatus("StressDown", user.stressDown - 1);
        }


        yield return action.performAction(user, new Character[] { target });
        if (controller.checkLife())
        {
            controller.nextTurn();

        }
    }


}
