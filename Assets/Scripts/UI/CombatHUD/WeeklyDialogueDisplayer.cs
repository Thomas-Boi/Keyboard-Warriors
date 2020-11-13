using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// display the dialogue if needed
public class WeeklyDialogueDisplayer : DialogueDisplayer
{
    public void Start()
    {
        dialogueData = GetDialogues();
        DisplayDialogue(dialogueData.onStartDialogue);
    }

    private DialogueData GetDialogues()
    {
        int weekNum = ProgressTracker.GetTracker().WeekNum;
        return base.GetDialogues("Dialogue/WeeklyCombat_wk" + weekNum);
    }

}
