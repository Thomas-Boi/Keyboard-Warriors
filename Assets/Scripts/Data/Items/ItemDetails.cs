using JetBrains.Annotations;
using System;
using UnityEngine;

[Serializable]
public struct ItemDetails
{
    public ItemDetail chocolate;
}

[Serializable]
public struct ItemDetail
{
    public string name;
    public string description;
    public int cost;
    public float amountPercent;
    public string targetType;
}
