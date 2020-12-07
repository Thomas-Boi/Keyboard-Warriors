using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDisplayer : MonoBehaviour
{
    private int index;
    private string[] tutorials;

    public Text tutorialTxt;

    public void Start()
    {
        index = 0;

        TextAsset tutorialJson = Resources.Load<TextAsset>($"Tutorial/tutorial");
        tutorials = JsonUtility.FromJson<TutorialData>(tutorialJson.ToString()).tutorials;

        
    }

    public void DisplayNextTutorial()
    {
        if (index + 1 >= tutorials.Length)
        {
            return;
        }
        tutorialTxt.text = tutorials[++index];
    }

    public void DisplayPrevTutorial()
    {
        if (index - 1 < 0)
        {
            return;
        }
        tutorialTxt.text = tutorials[--index];
    }

}


[Serializable]
struct TutorialData 
{
    public string[] tutorials;
}