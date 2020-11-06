using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateShopUI : MonoBehaviour
{

    public Text moneyTxtObj;

    void Start()
    {
        var tracker = ProgressTracker.GetTracker();
    }

    void Update()
    {
        moneyTxtObj.text = ItemTracker.GetTracker().Money.ToString();
    }

}
