using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpdateShopUI : MonoBehaviour
{

    public Text moneyTxtObj;

    public List<ItemButton> itemButtons;
    public Button foodOption;
    public Button drinksOption;
    public Button miscOption;
    public Button inventoryOption;

    private List<Item> storeItems;

    private ItemTracker itemTracker;

    void Start()
    {
        itemTracker = ItemTracker.GetTracker();

        InitStoreInventory();

        foodOption.onClick.AddListener(() => DisplayItems("Food"));
        drinksOption.onClick.AddListener(() => DisplayItems("Drink"));
        miscOption.onClick.AddListener(() => DisplayItems("Misc"));
        inventoryOption.onClick.AddListener(() => DisplayItems("Inv"));
    }

    void Update()
    {
        moneyTxtObj.text = itemTracker.Money.ToString();
    }

    private void InitStoreInventory()
    {
        storeItems = new List<Item>();
        var itemVals = Enum.GetValues(typeof(ItemsEnum)).Cast<ItemsEnum>();
        foreach (var item in itemVals)
        {
            storeItems.Add(ItemFactory.GetFactory().CreateItem(item));  
        }
    }

    private void DisplayItems(string type)
    {
        HideItemButtons();
        switch (type)
        {
            case "Food":
                DisplayFoodItems();
                break;
            case "Drink":
                DisplayDrinkItems();
                break;
            case "Misc":
                DisplayMiscItems();
                break;
            case "Inv":
                //DisplayPlayerInventory();
                break;
        }
    }

    private void HideItemButtons()
    {
        foreach (ItemButton itemButton in itemButtons)
        {
            itemButton.gameObject.SetActive(false);
        }
    }

    // food items that heal players
    private void DisplayFoodItems()
    {
        List<Item> foodItems = storeItems.FindAll(x => x.ItemType == ItemType.Food);

        for (int i = 0; i < foodItems.Count; i++)
        {
            itemButtons[i].SpawnButton(foodItems[i]);
            
        }
    }

    // drink items that destress players
    private void DisplayDrinkItems()
    {
        List<Item> drinkItems = storeItems.FindAll(x => x.ItemType == ItemType.Drink);
        for (int i = 0; i < drinkItems.Count; i++)
        {
            itemButtons[i].SpawnButton(drinkItems[i]);
        }
    }

    // misc. items that include reviving players
    private void DisplayMiscItems()
    {
        List<Item> miscItems = storeItems.FindAll(x => x.ItemType == ItemType.Misc);
        for (int i = 0; i < miscItems.Count; i++)
        {
            itemButtons[i].SpawnButton(miscItems[i]);
        }
    }

    private void DisplayPlayerInventory()
    {
        List<Item> items = EventController.ItemManager.GetItems();

        for (int i = 0; i < items.Count; i++)
        {
            itemButtons[i].SpawnButton(items[i]);
        }
    }

}
