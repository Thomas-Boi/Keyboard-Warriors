using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// display the dialogue if needed
public class DialogueDisplayer : MonoBehaviour
{
    // track the UI
    public GameObject dialoguePrefab;

    // track the dialogue data underneath
    private DialogueData dialogueData;

    private void Start()
    {
        dialogueData = GetDialogues();
        DisplayDialogue(dialogueData.onStart);
    }

    private DialogueData GetDialogues()
    {
        int weekNum = ProgressTracker.GetTracker().WeekNum;
        TextAsset dialogueJson = Resources.Load<TextAsset>("Dialogue/WeeklyCombat_wk" + weekNum);
        var dialogueMap = JsonUtility.FromJson<DialogueData>(dialogueJson.ToString());
        return dialogueMap;
    }

    // display a Dialogue box if there's one
    public void DisplayDialogue(DialogueStruct[] dialogues)
    {
        var dialogueElem = Instantiate(dialoguePrefab, transform).GetComponent<Dialogue>();
        dialogueElem.SetDialogues(dialogues);
        dialogueElem.NextDialogue();
    }
}
