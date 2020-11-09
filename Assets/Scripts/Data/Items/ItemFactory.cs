using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory
{
    // singleton object
    private static ItemFactory factory;

    // path to the itemDetails.json
    private static readonly string itemDetailsPath = "ItemDetails/items";
    private ItemDetails itemDetails;

    private ItemFactory()
    {
        TextAsset itemsJson = Resources.Load<TextAsset>(itemDetailsPath);
        itemDetails = JsonUtility.FromJson<ItemDetails>(itemsJson.ToString());
    }

    // get the singleton or init it
    public static ItemFactory GetFactory()
    {
        if (factory == null) factory = new ItemFactory();
        return factory;
    }

    // create an Item with the amount embedded in its alias
    public Item CreateItem(ItemsEnum item, int amount)
    {
        switch(item)
        {
            case ItemsEnum.Chocolate:
                return CreateHealingItem(itemDetails.Chocolate, amount);
            case ItemsEnum.Hamburger:
                return CreateHealingItem(itemDetails.Hamburger, amount);
            case ItemsEnum.Timbits:
                return CreateHealingItem(itemDetails.Timbits, amount);
            case ItemsEnum.Pizza:
                return CreateHealingItem(itemDetails.Pizza, amount);
            case ItemsEnum.Soda:
                return CreateDestressingItem(itemDetails.Soda, amount);
            case ItemsEnum.Milkshake:
                return CreateDestressingItem(itemDetails.Milkshake, amount);
            case ItemsEnum.Tea:
                return CreateDestressingItem(itemDetails.Tea, amount);
            case ItemsEnum.LightningBoltEnergyDrink:
                return CreateDestressingItem(itemDetails.LightningBoltEnergyDrink, amount);
            case ItemsEnum.FloppyDisk:
                return CreateReviveItem(itemDetails.FloppyDisk, amount);
            default:
                return CreateHealingItem(itemDetails.Chocolate, amount);
        }
    }


    private HealingItem CreateHealingItem(ItemDetail details, int amount)
    {
        string alias = CreateAlias(details.name, amount);
        return new HealingItem(
            details.name,
            details.description,
            details.targetType,
            alias,
            details.cost,
            details.amountPercent
            );
    }

    // create an alias for the object so it contains its name and amount
    private string CreateAlias(string name, int amount)
    {
        return $"{name}: {amount}";
    }

    private DestressingItem CreateDestressingItem(ItemDetail details, int amount)
    {

        string alias = CreateAlias(details.name, amount);
        return new DestressingItem(
            details.name,
            details.description,
            details.targetType,
            alias,
            details.cost,
            details.amountPercent
            );
    }

    private ReviveItem CreateReviveItem(ItemDetail details, int amount)
    {

        string alias = CreateAlias(details.name, amount);
        return new ReviveItem(
            details.name,
            details.description,
            details.targetType,
            alias,
            details.cost,
            details.amountPercent
            );
    }
}
