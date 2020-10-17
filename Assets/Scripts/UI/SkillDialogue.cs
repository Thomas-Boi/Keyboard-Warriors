using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDialogue : MonoBehaviour
{

    public Text skillText;

    public void AddSkillDialogue(GameObject HUD)
    {
        transform.SetParent(HUD.transform);
    }

    public void SetSkillText(string skillName)
    {
        skillText.text = skillName;

    }

    public void HideSkillDialogue(bool enabled)
    {
        gameObject.SetActive(enabled);
    }

}
