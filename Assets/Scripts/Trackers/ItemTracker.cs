using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemTracker
{
    // the singleton object
    private static ItemTracker tracker;

    // the money that the player has
    private int money;

    private Dictionary<ItemsEnum, int> inventory;

    // track how much money the player has
    public int Money
    {
        get => money;
    }

    private ItemTracker()
    {
        money = 0;
        inventory = new Dictionary<ItemsEnum, int>();
        //foreach (ItemsEnum itemEnum in Enum.GetValues(typeof(ItemsEnum)))
        //{
        //    inventory.Add(itemEnum, 1);
        //}

        // production code
        foreach (ItemsEnum itemEnum in Enum.GetValues(typeof(ItemsEnum)))
        {
            inventory.Add(itemEnum, 0);
        }

        // set the starting items for the player
        int defaultItemAmount = 2;
        inventory[ItemsEnum.Chocolate] = defaultItemAmount;
        inventory[ItemsEnum.Soda] = defaultItemAmount;
        inventory[ItemsEnum.FloppyDisk] = defaultItemAmount;
    }

    public static ItemTracker GetTracker()
    {
        if (tracker == null)
        {
            tracker = new ItemTracker();
        }
        return tracker;
    }


    // gain money for the player
    public void AddMoney(int amount)
    {
        if (amount > 0) money += amount;
    }

    // spend some money 
    public void SpendMoney(int amount)
    {
        if (amount <= money) money -= amount;
    }

    // get the amount of a specific item
    public int GetItemAmount(ItemsEnum item)
    {
        int value;
        return inventory.TryGetValue(item, out value) ? value : 0;
    }

    // add an item to the player's inventory
    public void AddItem(ItemsEnum item)
    {
        int value;
        if (inventory.TryGetValue(item, out value))
        {
            inventory[item] = ++value;
        }
    }

    // add an item to the player's inventory with it's string name
    public void AddItem(string item)
    {
        string itemNoSpace = item.Replace(" ", "");
        ItemsEnum itemEnum = (ItemsEnum)Enum.Parse(typeof(ItemsEnum), itemNoSpace);
        AddItem(itemEnum);
    }

    // remove an item from the player's inventory
    public void RemoveItem(ItemsEnum item)
    {
        int value;
        if (inventory.TryGetValue(item, out value))
        {
            inventory[item] = value - 1 < 0 ? 0 : --value;
        }
    }

    // get the inventory of anything that doesn't have an amount of zero
    public Dictionary<ItemsEnum, int> GetInventory()
    {
        var copy = new Dictionary<ItemsEnum, int>();
        foreach (ItemsEnum key in inventory.Keys)
        {
            if (inventory[key] != 0)
            {
                copy.Add(key, inventory[key]);
            }
        }
        return copy;
    }
}
