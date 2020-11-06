using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{

    public Item item;
    public string alias = "";

    void Awake()
    {
        gameObject.SetActive(false);   
    }

    void Start()
    {
          
    }

    public void SpawnButton(Item item)
    {
        this.item = item;
        GetComponentInChildren<Text>().text = item.Alias;
        gameObject.SetActive(true);
    }

    private void GetItemDetails()
    {
        
    }

}
