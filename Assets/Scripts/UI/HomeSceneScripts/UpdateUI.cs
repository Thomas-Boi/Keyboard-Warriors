using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    // the text objects
    public Text weekTxtObj;
    public Text scoreTxtObj;
    public Text moneyTxtObj;

    // Start is called before the first frame update
    // update the player's progress on the HomeScene
    void Start()
    {
        var tracker = ProgressTracker.GetTracker();
        weekTxtObj.text = $"Week {tracker.WeekNum} / {ProgressTracker.finalWeekNum}";
        scoreTxtObj.text = tracker.Score.ToString();
        moneyTxtObj.text = ItemTracker.GetTracker().Money.ToString();
    }
}
