using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public List<Spawner> spawners;
    public List<Character> players;
    public GameObject battleMenu;


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
                nextTurn();
                break;
                

            default:
                UnityEngine.Debug.Log("Invalid Wave");
                break;
        }
    }


    public void nextTurn()
    {
        UnityEngine.Debug.Log(playerTurn);
        if (playerTurn)
        {
            battleMenu.SetActive(true);
            turnNum++;
            if (turnNum >= players.Count)
            {
                turnNum = 0;
                playerTurn = false;
            }
        } 
        else if (!playerTurn)
        {
            spawners[turnNum].enemy.selectSkill();
            turnNum++;
            if (turnNum >= spawners.Count(x => x.isOccupied))
            {
                turnNum = 0;
                playerTurn = true;
            }
            nextTurn();
        }


        
    }


}
