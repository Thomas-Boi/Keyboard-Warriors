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

    public void Start()
    {
        dialogueData = GetDialogues();
        print(dialogueData);
        DisplayDialogue(dialogueData.onStartDialogue);
    }

    private DialogueData GetDialogues()
    {
        int weekNum = ProgressTracker.GetTracker().WeekNum;
        TextAsset dialogueJson = Resources.Load<TextAsset>("Dialogue/WeeklyCombat_wk" + weekNum);
        return JsonUtility.FromJson<DialogueData>(dialogueJson.ToString());
    }

    // display a Dialogue box if there's one
    public void DisplayDialogue(DialogueStruct[] dialogues)
    {
        var dialogueElem = Instantiate(dialoguePrefab, transform).GetComponent<Dialogue>();

        // show the first dialogue
        // after that, the onclick event handler will do the rest
        print(dialogues);
        dialogueElem.StartDialogue(dialogues, transform);
        dialogueElem.NextDialogue();
    }
}
