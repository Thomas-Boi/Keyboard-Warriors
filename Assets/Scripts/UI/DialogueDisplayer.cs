using System;
using UnityEngine;

public class DialogueDisplayer : MonoBehaviour
{
    // track the UI
    public GameObject dialoguePrefab;

    // track the dialogue data underneath
    protected DialogueData dialogueData;

    // blockMenu is for whether we want to display a panel to block the action menu
    protected void DisplayDialogue(DialogueStruct[] dialogues)
    {
        var dialogueElem = Instantiate(dialoguePrefab, transform).GetComponent<Dialogue>();
        dialogueElem.DialogueEnds += CleanUp; // register events

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
    protected virtual void CleanUp()
    {
    }
}
