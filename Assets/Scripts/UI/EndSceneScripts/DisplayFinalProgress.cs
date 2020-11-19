using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFinalProgress : MonoBehaviour
{
    public Text scoreTxtObj;
    public Text moneyTxtObj;

    // Start is called before the first frame update
    void Start()
    {
        string money = ItemTracker.GetTracker().Money.ToString();
        moneyTxtObj.text += money;
    }

}
