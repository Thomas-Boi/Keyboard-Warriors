using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// display the dialogue if needed
public class StoryDialogueDisplayer : DialogueDisplayer
{
    public void Start()
    {
        DialogueData dialogueData = GetDialogues();
        DisplayDialogue(dialogueData.onStartDialogue);
    }

    private DialogueData GetDialogues()
    {
        string jsonName = $"StoryDialogues/{ProgressTracker.GetTracker().StorylinePhase}";
        return base.GetDialogues(jsonName);
    }

    protected override void HandleDialogueEndsEvent()
    {
        SceneLoader.LoadHomeScene();
    }
}
