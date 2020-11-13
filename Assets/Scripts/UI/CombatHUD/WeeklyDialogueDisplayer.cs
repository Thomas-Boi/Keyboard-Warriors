using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// display the dialogue if needed
public class WeeklyDialogueDisplayer : DialogueDisplayer
{
    public GameObject menuBlockerPrefab;
    private GameObject menuBlocker;

    public void Start()
    {
        dialogueData = GetDialogues();
        DisplayDialogue(dialogueData.onStartDialogue);
        BlockMenu();
    }

    private DialogueData GetDialogues()
    {
        int weekNum = ProgressTracker.GetTracker().WeekNum;
        return base.GetDialogues("WeeklyCombat_wk" + weekNum);
    }

    private void BlockMenu()
    {
        menuBlocker = Instantiate(menuBlockerPrefab, transform);
    }

    // clean up after the dialogues finishes
    protected override void CleanUp()
    {
        Destroy(menuBlocker);
    }

}
