using JetBrains.Annotations;
using System;
using UnityEngine;

[Serializable]
public struct ItemDetails
{
    public ItemDetail Chocolate;
    public ItemDetail Hamburger;
    public ItemDetail Timbits;
    public ItemDetail Pizza;
    public ItemDetail Soda;
    public ItemDetail Milkshake;
    public ItemDetail Tea;
    public ItemDetail LightningBoltEnergyDrink;
    public ItemDetail FloppyDisk;
}

[Serializable]
public struct ItemDetail
{
    public string name;
    public string description;
    public int cost;
    public float amountPercent;
    public string targetType;
    public string itemType;
}
