﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// keep tracks of the items in the game.
// note that the items.json must have the "name" value
// as string version of the ItemsEnum for compatability
public enum ItemsEnum
{
    Chocolate,
    Hamburger,
    Timbits,
    Pizza,
    Soda,
    Milkshake,
    Tea,
    Gatorade,
    FloppyDisk
}

// track the type of each item
public enum ItemType
{
    Food,
    Drink,
    Misc
}