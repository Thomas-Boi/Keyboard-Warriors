using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HealingItem : Item
{
    public HealingItem(string name, string description,
        string targetType, int cost, float amountPercent) 
        : base(name, description, targetType, cost, amountPercent) { }

    override public IEnumerator performAction(Character user, Character[] targets)
    {
        Character target = targets[0];
        int amount = (int)(target.health * amountPercent);
        target.healHealth(amount);
        yield return new WaitForSeconds(.5f);
    }
}
