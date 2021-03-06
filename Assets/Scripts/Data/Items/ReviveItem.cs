﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// heal the player
public class ReviveItem : Item
{
    public ReviveItem(string name, string description,
        string targetType, string alias,
        int cost, float amountPercent, string itemType)
        : base(name, description, targetType, alias, cost, amountPercent, itemType) { }

    override public IEnumerator performAction(Character user, Character[] targets)
    {
        Character target = targets[0];
        int amount = (int)(target.maxHealth * amountPercent);
        target.healHealth(amount);
        RemoveItemFromInventory();
        PlayItemSound();
        yield return new WaitForSeconds(.5f);
    }
}
