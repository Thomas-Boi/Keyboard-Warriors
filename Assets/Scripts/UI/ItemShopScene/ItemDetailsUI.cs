using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsUI : MonoBehaviour
{
    public Text itemName;
    public Text itemCost;
    public Text itemDescription;
    public Text moneyWarning;

    public Button buyButton;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

}
