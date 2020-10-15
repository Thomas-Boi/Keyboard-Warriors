using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// display the dialogue if needed
public class DialogueDisplayer : MonoBehaviour
{
    public GameObject dialoguePrefab;

    private void Start()
    {
        var dialogues = GetDialogues();
        DisplayDialogue(dialogues);
    }

    private DialogueStruct[] GetDialogues()
    {
        int weekNum = ProgressTracker.GetTracker().WeekNum;
        TextAsset dialogueJson = Resources.Load<TextAsset>("Dialogue/CombatDialogues");
        var dialogueMap = JsonConvert.DeserializeObject<Dictionary<int, DialogueStruct[]>>(dialogueJson.ToString());
        return dialogueMap[weekNum];
    }

    // display a Dialogue box if there's one
    public void DisplayDialogue(DialogueStruct[] dialogues)
    {
        var dialogueElem = Instantiate(dialoguePrefab, transform).GetComponent<Dialogue>();
        dialogueElem.SetDialogues(dialogues);
        dialogueElem.NextDialogue();
    }
}
