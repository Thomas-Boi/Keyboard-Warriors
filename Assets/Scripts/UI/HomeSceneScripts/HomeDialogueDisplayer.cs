using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// display the dialogue if needed
public class HomeDialogueDisplayer : DialogueDisplayer
{
    private static int weekAlreadyDisplayed = 0;

    public void Start()
    {
        if (ProgressTracker.GetTracker().WeekNum == weekAlreadyDisplayed)
        {
            return;
        }
        DialogueData dialogueData = GetDialogues();
        DisplayDialogue(dialogueData.onStartDialogue);
        weekAlreadyDisplayed = ProgressTracker.GetTracker().WeekNum;
    }

    private DialogueData GetDialogues()
    {
        string jsonName = $"HomeSceneDialogues/Home_wk{ProgressTracker.GetTracker().WeekNum}";
        return base.GetDialogues(jsonName);
    }
}
