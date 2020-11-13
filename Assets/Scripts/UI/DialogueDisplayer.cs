using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDisplayer : MonoBehaviour
{
    // track the UI
    public GameObject dialoguePrefab;

    // track the dialogue data underneath
    protected DialogueData dialogueData;

    // display a Dialogue box if there's one
    protected void DisplayDialogue(DialogueStruct[] dialogues)
    {
        var dialogueElem = Instantiate(dialoguePrefab, transform).GetComponent<Dialogue>();

        // show the first dialogue
        // after that, the onclick event handler will do the rest
        dialogueElem.StartDialogue(dialogues, transform);
        dialogueElem.NextDialogue();
    }

    // get the dialogues from Resources/Dialogue folder
    protected DialogueData GetDialogues(string jsonName)
    {
        TextAsset dialogueJson = Resources.Load<TextAsset>($"Dialogue/{jsonName}");
        return JsonUtility.FromJson<DialogueData>(dialogueJson.ToString());
    }
}
