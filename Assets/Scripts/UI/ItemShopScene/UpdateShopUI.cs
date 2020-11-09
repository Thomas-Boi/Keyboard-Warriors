using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UpdateShopUI : MonoBehaviour
{

    public Text moneyTxtObj;

    public List<ItemButton> itemButtons;
    public Button foodOption;
    public Button drinksOption;
    public Button miscOption;

    //public ItemDetailsUI itemDetailsUI;

    private List<Item> foodItems;
    private List<Item> drinkItems;
    private List<Item> miscItems;

    private ItemTracker itemTracker;

    void Start()
    {
        itemTracker = ItemTracker.GetTracker();

        InitStore();

        foodOption.onClick.AddListener(() => DisplayItems("Food"));
        drinksOption.onClick.AddListener(() => DisplayItems("Drink"));
        miscOption.onClick.AddListener(() => DisplayItems("Misc"));
    }

    void Update()
    {
        moneyTxtObj.text = itemTracker.Money.ToString();
    }

    // todo; find a more effective way to add items on start
    private void InitStore()
    {
        // food items
        foodItems = new List<Item>();
        foodItems.Add(ItemFactory.GetFactory().CreateItem(ItemsEnum.Chocolate));
        foodItems.Add(ItemFactory.GetFactory().CreateItem(ItemsEnum.Hamburger));
        foodItems.Add(ItemFactory.GetFactory().CreateItem(ItemsEnum.Timbits));
        foodItems.Add(ItemFactory.GetFactory().CreateItem(ItemsEnum.Pizza));

        // drink items
        drinkItems = new List<Item>();
        drinkItems.Add(ItemFactory.GetFactory().CreateItem(ItemsEnum.Soda));
        drinkItems.Add(ItemFactory.GetFactory().CreateItem(ItemsEnum.Milkshake));
        drinkItems.Add(ItemFactory.GetFactory().CreateItem(ItemsEnum.Tea));
        drinkItems.Add(ItemFactory.GetFactory().CreateItem(ItemsEnum.LightningBoltEnergyDrink));

        // misc. items
        miscItems = new List<Item>();
        miscItems.Add(ItemFactory.GetFactory().CreateItem(ItemsEnum.FloppyDisk));
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
        for (int i = 0; i < foodItems.Count; i++)
        {
            itemButtons[i].SpawnButton(foodItems[i]);
        }
    }

    // drink items that destress players
    private void DisplayDrinkItems()
    {
        for (int i = 0; i < drinkItems.Count; i++)
        {
            itemButtons[i].SpawnButton(drinkItems[i]);
        }
    }

    // misc. items that include reviving players
    private void DisplayMiscItems()
    {
        for (int i = 0; i < miscItems.Count; i++)
        {
            itemButtons[i].SpawnButton(miscItems[i]);
        }
    }

}
