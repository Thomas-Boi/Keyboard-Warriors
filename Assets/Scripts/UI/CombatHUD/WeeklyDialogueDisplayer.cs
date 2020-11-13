using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// display the dialogue if needed
public class WeeklyDialogueDisplayer : DialogueDisplayer
{
    public GameObject choiceMenu;

    public void Start()
    {
        if (ProgressTracker.GetTracker().ProductionMode)
        {
            dialogueData = GetDialogues();
            DisplayDialogue(dialogueData.onStartDialogue);
            // hide the choice menu so player can play while dialogue is open
            choiceMenu.SetActive(false);
        }
    }

    private DialogueData GetDialogues()
    {
        int weekNum = ProgressTracker.GetTracker().WeekNum;
        return base.GetDialogues("WeeklyCombat_wk" + weekNum);
    }

    // clean up after the dialogues finishes
    protected override void HandleDialogueEndsEvent()
    {
        choiceMenu.SetActive(true);
    }

}
