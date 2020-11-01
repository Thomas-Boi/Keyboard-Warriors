using System;

// represent an Item that can be used by the player
public abstract class Item : Action
{
    // cost in gold to buy this item
    protected int cost;

    // the percentage that the item will heal/destress by
    protected float amountPercent;

    public int Cost { get => cost; }

    public float AmountPercent { get => amountPercent; }

    public Item(string name, string description,
        string targetType, string alias,
        int cost, float amountPercent) : base(name, description, targetType, alias)
    {
        this.cost = cost;
        this.amountPercent = amountPercent;
    }

    // remove this item from inventory
    protected void RemoveItemFromInventory()
    {
        ItemsEnum itemEnum = (ItemsEnum)Enum.Parse(typeof(ItemsEnum), name);
        ItemTracker.GetTracker().RemoveItem(itemEnum);
    }
}



