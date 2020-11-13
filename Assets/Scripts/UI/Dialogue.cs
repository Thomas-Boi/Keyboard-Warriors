using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// holds the Text elements to display a dialogue piece from characters
public class Dialogue : MonoBehaviour
{
    // refs to UI elements
    public Text speakerTxt;
    public Text speechTxt;

    // hold the dialogues
    private DialogueStruct[] dialogues;
    private int curScriptIndex;

    // hold the MenusBlocker
    public GameObject menuBlockerPrefab;
    private GameObject menuBlocker;

    // start a new series of dialogues.
    // need to call this first after instantiaing a Dialogue Prefab
    // the combatHUDTransform is the UI canvas element's transform
    public void StartDialogue(DialogueStruct[] dialogues, Transform combatHUDTransform)
    {
        this.dialogues = dialogues;
        curScriptIndex = 0;
        menuBlocker = Instantiate(menuBlockerPrefab, combatHUDTransform);
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
            Destroy(menuBlocker);
        }
        catch (NullReferenceException)
        {
            Destroy(gameObject);
            Destroy(menuBlocker);
        }
        
    }

}

