using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplayer : MonoBehaviour
{
    // track the UI
    public GameObject dialoguePrefab;

    // blockMenu is for whether we want to display a panel to block the action menu
    protected void DisplayDialogue(DialogueStruct[] dialogues)
    {
        var dialogueElem = Instantiate(dialoguePrefab, transform).GetComponent<Dialogue>();
        dialogueElem.DialogueEnds += HandleDialogueEndsEvent; // register events

        // show the first dialogue
        // after that, the onclick event handler will do the rest
        dialogueElem.StartDialogue(dialogues);
        dialogueElem.NextDialogue();
    }

    // get the dialogues from Resources/Dialogue folder
    protected DialogueData GetDialogues(string jsonName)
    {
        TextAsset dialogueJson = Resources.Load<TextAsset>($"Dialogue/{jsonName}");
        return JsonUtility.FromJson<DialogueData>(dialogueJson.ToString());
    }

    // clean up after the dialogues finishes
    protected virtual void HandleDialogueEndsEvent()
    {
    }
}
