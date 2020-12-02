using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressBar : MonoBehaviour
{
    public Slider slider;
    public Text stressText;

    //private string stressStr;

    void Update()
    {
        UpdateStressString();
    }

    private void UpdateStressString()
    {
        int curStress = (int)slider.value;
        int maxStress = (int)slider.maxValue;
        string stressStr = curStress + " / " + maxStress;
        stressText.text = stressStr;
    }

    public void SetMaxStress(float stress)
    {
        slider.maxValue = stress;
    }

    public void SetStress(float stress)
    {
        slider.value = stress;
    }
}
