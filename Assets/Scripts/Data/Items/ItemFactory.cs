using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory
{
    // singleton object
    private static ItemFactory factory;

    // path to the itemDetails.json
    private static readonly string itemDetailsPath = "Items/items";
    private ItemDetails itemDetails;

    private ItemFactory()
    {
        TextAsset itemsJson = Resources.Load<TextAsset>(itemDetailsPath);
        itemDetails = JsonUtility.FromJson<ItemDetails>(itemsJson.ToString());
    }

    // get the singleton or init it
    public ItemFactory getFactory()
    {
        if (factory == null) factory = new ItemFactory();
        return factory;
    }

    public HealingItem createChocolate()
    {
        ItemDetail chocolate = itemDetails.chocolate;
        return new HealingItem(
            chocolate.name,
            chocolate.description,
            chocolate.targetType,
            chocolate.cost,
            chocolate.amountPercent
            );
    }
}
