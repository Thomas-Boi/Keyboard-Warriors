using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxStress(float stress)
    {
        slider.maxValue = stress;
    }

    public void SetStress(float stress)
    {
        slider.value = stress;
    }
}
