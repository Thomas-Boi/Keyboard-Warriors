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
        var tracker = ProgressTracker.GetTracker();
        scoreTxtObj.text += tracker.Score.ToString();
        moneyTxtObj.text += tracker.Money.ToString();
    }

}
