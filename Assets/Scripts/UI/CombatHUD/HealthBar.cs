using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Text hpText;
    public bool isEnemy;

    //private string hpStr;

    void Start()
    {
        if (isEnemy)
        {
            hpText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!isEnemy)
        {
            UpdateHPString();
        }
    }

    private void UpdateHPString()
    {
        int curHp = (int)slider.value;
        int maxHp = (int)slider.maxValue;
        string hpStr = curHp + " / " + maxHp;
        hpText.text = hpStr;
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
