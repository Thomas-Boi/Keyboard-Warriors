using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressUpdater : MonoBehaviour
{
    // get the rewards for winning then move to the next scene
    // increment the week
    // if it's the final week and they pass, display an end scene
    public void GetWinRewards()
    {
        int money = 100;
        UpdateProgress(money);

        // see if we need to progress to next week
        var tracker = ProgressTracker.GetTracker();
        if (tracker.CurCombatType == SceneTypeEnum.WEEKLY_COMBAT)
        {
            int curWeek = tracker.WeekNum;
            if (curWeek == ProgressTracker.finalWeekNum)
            {
                SceneLoader.LoadEndScene();
                return;
            }
            else tracker.NextWeek();
        }
        SceneLoader.LoadHomeScene();
    }

    // update the player's score and money
    private void UpdateProgress(int money)
    {
        ItemTracker.GetTracker().AddMoney(money);
    }

    // get the rewards for losing then move to the next scene
    public void GetLoseRewards()
    {
        int money = 10;
        UpdateProgress(money);
        SceneLoader.LoadHomeScene();
    }
}
