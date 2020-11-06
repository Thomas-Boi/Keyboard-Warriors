using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateShopUI : MonoBehaviour
{

    public Text moneyTxtObj;
    public List<Button> itemOptions;

    private ProgressTracker tracker;
    private ItemTracker itemTracker;

    void Start()
    {
        tracker = ProgressTracker.GetTracker();
        itemTracker = ItemTracker.GetTracker();
    }

    void Update()
    {
        UpdateMoneyText();

    }

    private void UpdateMoneyText()
    {
        moneyTxtObj.text = itemTracker.Money.ToString();
    }

}
