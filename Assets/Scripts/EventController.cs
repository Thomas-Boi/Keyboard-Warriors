﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour
{
    public List<Spawner> spawners;
    public List<Character> players;
    public List<HealthBar> healthbars;
    public List<SkillButton> skillButtons;
    public GameObject battleMenu;
    public GameObject winUIPrefab;
    public GameObject loseUIPrefab;
    public GameObject HUD;

    // true = player turn
    // false = not player turn
    public bool playerTurn;
    //turn number within each team.
    // 0 = player1, 1 = player2
    public int turnNum = -1;
    public string selectedSkill = "";
    public int waveNum;
    

    // Start is called before the first frame update
    void Start()
    {
        startWave(waveNum);
    }



    public void startWave(int wave)
    {
        turnNum = -1;
        switch (wave)
        {
            case 1:
                spawners[0].spawn("boxSlime");
                spawners[1].spawn("boxSlimeSmall");
                spawners[2].spawn("boxSlimeSmall");
                spawners[4].spawn("boxSlime");

                for (int i = 0; i < spawners.Count; i++)
                {
                    if (spawners[i].isOccupied)
                    {
                        spawners[i].enemy.healthBar = healthbars[i];
                    } 
                }

                nextTurn();
                break;
                

            default:
                UnityEngine.Debug.Log("Invalid Wave");
                break;
        }
    }


    public void displaySkills()
    {
        //these lists are temporary solutions. Will refactor and move this somewhere else later
        List<string> playerOne = new List<string> { "basicAttack", "basicAttack", "slimeyAttack" };
        List<string> playerTwo = new List<string> { "basicAttack", "basicAttack", "slimeyAttack", "slimeyAttack" };

        if (turnNum == 0)
        {
            for (int i = 0; i < playerOne.Count; i++)
            {
                skillButtons[i].spawnButton(playerOne[i], "p1Attack " + i);
            }
        }
        else
        {
            for (int i = 0; i < playerTwo.Count; i++)
            {
                skillButtons[i].spawnButton(playerTwo[i], "p2Attack " + i);
            }
        }
        
    }

    public void hideSkills()
    {
        foreach(SkillButton button in skillButtons)
        {
            button.gameObject.SetActive(false);
        }
    }




    public void nextTurn()
    {
        hideSkills();
        turnNum++;
        if (playerTurn)
        {
            displaySkills();
            if (turnNum >= players.Count)
            {
                turnNum = 0;
                playerTurn = false;
            }
        } 
        if (!playerTurn)
        {
            if (turnNum >= spawners.Count)
            {
                turnNum = -1;
                playerTurn = true;
                nextTurn();
            }
            else if (spawners[turnNum].isOccupied)
            {
                spawners[turnNum].enemy.selectSkill();
            }
            else
            {
                nextTurn();
            }

        }

        
        
    }

    public bool checkLife()
    {
        for (int i = 0; i < spawners.Count; i++)
        {
            if (spawners[i].isOccupied && spawners[i].enemy.health <= 0)
            {
                spawners[i].kill();
            }
        }
        if (players.Sum(p => p.health) <= 0)
        {
            displayPlayerLose();
            return false;
        }
        if (spawners.Count(s => s.isOccupied) <= 0)
        {
            displayPlayerWin();
            return false;
        }
        return true;
    }

    
    public IEnumerator DisplayDamage(Character target, float damage)
    {
        // get position of target
        Vector3 charPosition = target.getCharacterPosition();
        //UnityEngine.Debug.Log(charPosition);

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(charPosition);
        //UnityEngine.Debug.Log(screenPosition);

        GameObject textObj = new GameObject("DamageText", typeof(RectTransform));
        textObj.transform.SetParent(HUD.transform);

        Text damageText = textObj.AddComponent<Text>();
        damageText.text = damage.ToString();
        damageText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        damageText.color = Color.black;
        damageText.fontSize = 35;
        damageText.horizontalOverflow = HorizontalWrapMode.Overflow;
        damageText.verticalOverflow = VerticalWrapMode.Overflow;

        textObj.transform.position = screenPosition;

        yield return new WaitForSeconds(.5f);

        Destroy(textObj);
    }


    private void displayPlayerWin()
    {
        GameObject wonPanel = Instantiate(winUIPrefab, HUD.transform);
    }

    private void displayPlayerLose()
    {
        GameObject losePanel = Instantiate(loseUIPrefab, HUD.transform);
    }

}
