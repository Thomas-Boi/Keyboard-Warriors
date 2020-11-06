using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateShopUI : MonoBehaviour
{

    public Text moneyTxtObj;

    public List<ItemButton> itemButtons;
    public Button foodOption;
    public Button drinksOption;
    public Button miscOption;

    public ItemDetailsUI itemDetailsUI;

    public static List<Item> foodItems;
    public static List<Item> drinkItems;
    public static List<Item> miscItems;

    // trackers
    private ProgressTracker tracker;
    private ItemTracker itemTracker;

    void Start()
    {
        tracker = ProgressTracker.GetTracker();
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
        foodItems.Add(ItemFactory.GetFactory().CreateItem(ItemsEnum.Chocolate, 99));

        // drink items
        drinkItems = new List<Item>();
        drinkItems.Add(ItemFactory.GetFactory().CreateItem(ItemsEnum.Milkshake, 99));

        // misc. items
        miscItems = new List<Item>();
        // add misc items here
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
        /*for (int i = 0; i < drinkItems.Count; i++)
        {
            itemButtons[i].SpawnButton(miscItems[i]);
        }*/
    }

}
