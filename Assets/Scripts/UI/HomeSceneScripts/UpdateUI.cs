using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    // the text objects
    public Text weekTxtObj;
    public Text moneyTxtObj;

    public List<Button> panels;

    // Start is called before the first frame update
    // update the player's progress on the HomeScene
    void Start()
    {
        var tracker = ProgressTracker.GetTracker();
        weekTxtObj.text = $"Week {tracker.WeekNum} / {ProgressTracker.finalWeekNum}";
        moneyTxtObj.text = ItemTracker.GetTracker().Money.ToString();

        if (tracker.ProductionMode)
        {
            foreach (Button panel in panels)
            {
                if (tracker.WeekNum == 1)
                {
                    if (panel.name == "GoToClassPanel") continue;
                }
                else if (tracker.WeekNum == 2)
                {
                    if (panel.name == "GoToClassPanel" || panel.name == "ExtraStudyPanel" || panel.name == "BCITStorePanel") continue;
                }
                panel.interactable = false;
            }
        }
    }
}
