using System;
using System.Diagnostics;

// represent an Item that can be used by the player
public abstract class Item : Action
{
    // cost in gold to buy this item
    protected int cost;

    // the percentage that the item will heal/destress by
    protected float amountPercent;

    // the ItemType of this Item
    protected ItemType itemType;

    public int Cost { get => cost; }
    public float AmountPercent { get => amountPercent; }
    public ItemType ItemType { get => itemType; }

    public Item(string name, string description,
        string targetType, string alias,
        int cost, float amountPercent,
        string itemType) : base(name, description, targetType, alias)
    {
        this.cost = cost;
        this.amountPercent = amountPercent;
        // cast the string of targetType to enum form
        ItemType enumItemType;
        try
        {
            enumItemType = (ItemType)Enum.Parse(typeof(ItemType), itemType);
        }
        catch
        {
            enumItemType = ItemType.Misc;
        }
        this.itemType = enumItemType;
    }

    // remove this item from inventory
    protected void RemoveItemFromInventory()
    {
        string noSpaceName = name.Replace(" ", "");
        ItemsEnum itemEnum = (ItemsEnum)Enum.Parse(typeof(ItemsEnum), noSpaceName);
        ItemTracker.GetTracker().RemoveItem(itemEnum);
    }
}



