using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// display the dialogue if needed
public class StoryDialogueDisplayer : DialogueDisplayer
{
    public void Start()
    {
        dialogueData = GetDialogues();
        DisplayDialogue(dialogueData.onStartDialogue);
    }

    private DialogueData GetDialogues()
    {
        string jsonName = $"{ProgressTracker.GetTracker().StorylinePhase}Dialogues";
        return base.GetDialogues(jsonName);
    }

    protected override void HandleDialogueEndsEvent()
    {
        SceneLoader.LoadHomeScene();
    }
}
