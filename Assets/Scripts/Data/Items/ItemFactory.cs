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
            details.amountPercent,
            details.itemType
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
            details.amountPercent,
            details.itemType
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
            details.amountPercent,
            details.itemType
            );
    }


    // create an Item with its name as the alias
    public Item CreateItem(ItemsEnum item)
    {
        switch (item)
        {
            case ItemsEnum.Chocolate:
                return CreateHealingItem(itemDetails.Chocolate);
            case ItemsEnum.Hamburger:
                return CreateHealingItem(itemDetails.Hamburger);
            case ItemsEnum.Timbits:
                return CreateHealingItem(itemDetails.Timbits);
            case ItemsEnum.Pizza:
                return CreateHealingItem(itemDetails.Pizza);
            case ItemsEnum.Soda:
                return CreateDestressingItem(itemDetails.Soda);
            case ItemsEnum.Milkshake:
                return CreateDestressingItem(itemDetails.Milkshake);
            case ItemsEnum.Tea:
                return CreateDestressingItem(itemDetails.Tea);
            case ItemsEnum.LightningBoltEnergyDrink:
                return CreateDestressingItem(itemDetails.LightningBoltEnergyDrink);
            case ItemsEnum.FloppyDisk:
                return CreateReviveItem(itemDetails.FloppyDisk);
            default:
                return CreateHealingItem(itemDetails.Chocolate);
        }
    }

    private HealingItem CreateHealingItem(ItemDetail details)
    {
        return new HealingItem(
            details.name,
            details.description,
            details.targetType,
            details.name,
            details.cost,
            details.amountPercent,
            details.itemType
            );
    }

    private DestressingItem CreateDestressingItem(ItemDetail details)
    {
        return new DestressingItem(
            details.name,
            details.description,
            details.targetType,
            details.name,
            details.cost,
            details.amountPercent,
            details.itemType
            );
    }

    private ReviveItem CreateReviveItem(ItemDetail details)
    {
        return new ReviveItem(
            details.name,
            details.description,
            details.targetType,
            details.name,
            details.cost,
            details.amountPercent,
            details.itemType
            );
    }
}
