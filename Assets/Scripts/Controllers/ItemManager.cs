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


}
