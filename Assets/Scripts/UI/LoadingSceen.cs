using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceen : MonoBehaviour
{
    public Text loadingTxt;

    private void Start()
    {
        // need to check which scene we are loading
        displayForCombatScene();
    }


    // display the loading text for the CombatScene
    void displayForCombatScene()
    {
        string[] flavourTxts = { 
            "Riding the bus",
            "Going to the Library",
            "Taking the SkyTrain",
            "Walking to SE2"
        };

        int index = new System.Random().Next(flavourTxts.Length);
        displayText(flavourTxts[index]);
    }

    void displayText(string txt)
    {
        loadingTxt.text = txt + "...";
    }
}
