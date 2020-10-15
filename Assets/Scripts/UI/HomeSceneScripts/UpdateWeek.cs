using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWeek : MonoBehaviour
{
    public Text weekTxt;

    // Start is called before the first frame update
    void Start()
    {
        int weekNum = ProgressTracker.GetTracker().WeekNum;
        weekTxt.text = $"Week {weekNum} / 6";
    }

}
