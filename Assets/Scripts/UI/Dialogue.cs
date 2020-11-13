using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void EventHandler();

// holds the Text elements to display a dialogue piece from characters
public class Dialogue : MonoBehaviour
{
    // refs to UI elements
    public Text speakerTxt;
    public Text speechTxt;

    // hold the dialogues
    private DialogueStruct[] dialogues;
    private int curScriptIndex;

    // event handler
    public event EventHandler DialogueEnds;

    // start a new series of dialogues.
    // need to call this first after instantiaing a Dialogue Prefab
    // the combatHUDTransform is the UI canvas element's transform
    public void StartDialogue(DialogueStruct[] dialogues)
    {
        this.dialogues = dialogues;
        curScriptIndex = 0;
    }


    // display the texts in the dialogueObj on the screen
    // if there's none, delete the dialogue
    public void NextDialogue()
    {
        try 
        {
            DialogueStruct dialogueObj = dialogues[curScriptIndex++];
            speakerTxt.text = dialogueObj.speaker;
            speechTxt.text = dialogueObj.speech;
        } 
        catch (IndexOutOfRangeException)
        {
            dialogues = null;
            Destroy(gameObject);
            OnDialogueEnds();
        }
        catch (NullReferenceException)
        {
            Destroy(gameObject);
            OnDialogueEnds();
        }
        
    }

    protected virtual void OnDialogueEnds()
    {
        DialogueEnds();
    }

}

