using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// destress the player
public class DestressingItem : Item
{
    public DestressingItem(string name, string description,
        string targetType, string alias,
        int cost, float amountPercent, string itemType)
        : base(name, description, targetType, alias, cost, amountPercent, itemType) { }

    override public IEnumerator performAction(Character user, Character[] targets)
    {

        if (targetType == TargetType.ALLY_SINGLE)
        {
            Character target = targets[0];
            int amount = (int)(target.maxStress * amountPercent);
            target.SetCharacterStress(target.stress - amount);
        }
        else if (targetType == TargetType.ALLY_ALL)
        {
            foreach (Character target in targets)
            {
                int amount = (int)(target.maxStress * amountPercent);
                target.SetCharacterStress(target.stress - amount);
            }
        }
        PlayItemSound();
        RemoveItemFromInventory();
        yield return new WaitForSeconds(.5f);
    }

}
