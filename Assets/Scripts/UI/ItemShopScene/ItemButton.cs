using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{

    public ItemDetail item;
    public string itemName = "";

    void Awake()
    {
        gameObject.SetActive(false);    
    }

    public void SpawnButton(ItemDetail item)
    {
        this.item = item;
        GetComponentInChildren<Text>().text = item.name;
        gameObject.SetActive(true);
    }

}
