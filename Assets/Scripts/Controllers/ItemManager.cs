using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : ActionManager
{
    private ItemFactory itemFactory;

    void Start()
    {
        itemFactory = ItemFactory.GetFactory();
        initController();
    }

    // get the items that the player has 
    public List<Item> GetItems()
    {
        Dictionary<ItemsEnum, int> inventory = ItemTracker.GetTracker().GetInventory();
        List<Item> items = new List<Item>();
        ItemFactory factory = ItemFactory.GetFactory();

        foreach (ItemsEnum key in inventory.Keys)
        {
            items.Add(factory.CreateItem(key, inventory[key]));
        }

        return items;
    }
}
