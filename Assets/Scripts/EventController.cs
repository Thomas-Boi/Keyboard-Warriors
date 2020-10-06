﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public List<Spawner> spawners;
    public List<Character> players;
    public List<HealthBar> healthbars;
    public GameObject battleMenu;
    public GameObject winUIPrefab;
    public GameObject loseUIPrefab;
    public GameObject HUD;


    // true = player turn
    // false = not player turn
    public bool playerTurn;
    //turn number within each team.
    // 0 = player1, 1 = player2
    public int turnNum = 0;

    public int waveNum;
    

    // Start is called before the first frame update
    void Start()
    {
        startWave(waveNum);
    }

    public void startWave(int wave)
    {
        switch(wave)
        {
            case 1:
                spawners[0].spawn("boxSlime");
                spawners[1].spawn("boxSlimeSmall");
                spawners[2].spawn("boxSlimeSmall");

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


    public void nextTurn()
    {
        turnNum++;
        if (playerTurn)
        {
            //battleMenu.SetActive(true);
            if (turnNum >= players.Count)
            {
                turnNum = 0;
                playerTurn = false;
            }
        } 
        else if (!playerTurn)
        {
            if (turnNum-1 >= spawners.Count)
            {
                turnNum = 0;
                playerTurn = true;
                nextTurn();
            }
            else if (spawners[turnNum-1].isOccupied)
            {
                spawners[turnNum-1].enemy.selectSkill();
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
            if (spawners[i].enemy.health <= 0)
            {
                spawners[i].kill();
            }
        }
        if (players.Sum(p => p.health) <= 0)
        {
            displayPlayerLose();
            return false;
        }
        if (spawners.Sum(s => s.enemy.health) <= 0)
        {
            displayPlayerWin();
            return false;
        }
        return true;
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
