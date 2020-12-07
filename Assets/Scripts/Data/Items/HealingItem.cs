using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// heal the player
public class HealingItem : Item
{
    public HealingItem(string name, string description,
        string targetType, string alias,
        int cost, float amountPercent, string itemType) 
        : base(name, description, targetType, alias, cost, amountPercent, itemType) { }

    override public IEnumerator performAction(Character user, Character[] targets)
    {
        
        if (targetType == TargetType.ALLY_SINGLE)
        {
            Character target = targets[0];
            int amount = (int)(target.maxHealth * amountPercent);
            target.healHealth(amount);
        }
        else if (targetType == TargetType.ALLY_ALL)
        {
            EventController controller = GameObject.Find("EventController").GetComponent<EventController>();
            foreach (Character player in controller.GetAlivePlayers())
            {
                int amount = (int)(player.maxHealth * amountPercent);
                player.healHealth(amount);
            }
        }

        RemoveItemFromInventory();
        PlayItemSound();
        yield return new WaitForSeconds(.5f);
    }
}
