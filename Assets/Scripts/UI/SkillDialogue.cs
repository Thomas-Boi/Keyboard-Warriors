using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDialogue : MonoBehaviour
{

    public Text skillText;

    private void SetSkillText(string skillName)
    {
        skillText.text = skillName;
    }

    private void ShowSkillDialogue(bool enabled)
    {
        if (GameObject.Find("Dialogue(Clone)"))
        {
            transform.SetAsLastSibling();
        }
        gameObject.SetActive(enabled);
    }

    public IEnumerator DisplaySkill(string skillName, float duration)
    {
        SetSkillText(skillName);
        ShowSkillDialogue(true);

        yield return new WaitForSeconds(duration);

        ShowSkillDialogue(false);
    }

}
