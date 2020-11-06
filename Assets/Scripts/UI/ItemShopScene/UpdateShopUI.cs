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

    // trackers
    private ProgressTracker tracker;
    private ItemTracker itemTracker;

    void Start()
    {
        tracker = ProgressTracker.GetTracker();
        itemTracker = ItemTracker.GetTracker();
    }

    void Update()
    {
        moneyTxtObj.text = itemTracker.Money.ToString();
    }

    private void DisplayItems(bool enabled)
    {
        foreach (ItemButton itemButton in itemButtons)
        {
            itemButton.gameObject.SetActive(enabled);
        }
    }

}
