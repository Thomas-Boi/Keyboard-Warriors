using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// display the dialogue if needed
public class BCITDialogueDisplayer : DialogueDisplayer
{
    public void Start()
    {
        DialogueData dialogueData = GetDialogues();
        DisplayDialogue(dialogueData.onStartDialogue);
    }

    private DialogueData GetDialogues()
    {
        string jsonName = $"StoryDialogues/BCITIntro";
        return base.GetDialogues(jsonName);
    }

    protected override void HandleDialogueEndsEvent()
    {
        SceneLoader.LoadStoryScene();
    }
}
