using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{

    public Item item;
    public string alias = "";

    private ItemDetailsUI itemDetailsUI;

    void Awake()
    {
        gameObject.SetActive(false);   
    }

    void Start()
    {
        itemDetailsUI = GameObject.Find("ItemDetailsUI").GetComponent<ItemDetailsUI>();
        GetComponent<Button>().onClick.AddListener(() => SetItemDetails());
    }

    public void SpawnButton(Item item)
    {
        this.item = item;
        GetComponentInChildren<Text>().text = item.Alias;
        gameObject.SetActive(true);
    }

    private void SetItemDetails()
    {
        itemDetailsUI.moneyWarning.gameObject.SetActive(false);
        itemDetailsUI.item = item;
    }

}
