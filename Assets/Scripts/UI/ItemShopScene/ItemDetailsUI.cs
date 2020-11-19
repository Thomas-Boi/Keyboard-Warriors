using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemDetailsUI : MonoBehaviour
{
    public Text itemName;
    public Text itemCost;
    public Text itemDescription;
    public Text moneyWarning;
    public Button buyButton;

    public Item item;

    private ItemTracker itemTracker;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // when scene is loaded reset item to null
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        moneyWarning.gameObject.SetActive(false);
        item = null;
    }

    void Start()
    {
        itemTracker = ItemTracker.GetTracker();

        buyButton.onClick.AddListener(() => PurchaseItem());
    }

    void Update()
    {

        UpdateItemDetails();
        
    }

    public void UpdateItemDetails()
    {
        if (item != null)
        {
            itemName.text = item.Name;
            itemCost.text = "Cost: " + item.Cost;
            itemDescription.text = item.Description;
            buyButton.gameObject.SetActive(true);
        } else
        {
            itemName.text = "";
            itemCost.text = "";
            itemDescription.text = "";
            buyButton.gameObject.SetActive(false);
        }
    }

    public void PurchaseItem()
    {

        if (itemTracker.Money < item.Cost)
        {
            moneyWarning.gameObject.SetActive(true);
        } else
        {
            itemTracker.AddItem(item.Name);
            itemTracker.SpendMoney(item.Cost);
            moneyWarning.gameObject.SetActive(false);
        }

    }

}
