using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public List<Spawner> spawners;


    public List<Character> players;

    // true = player turn
    // false = not player turn
    public bool playerTurn = false;
    //turn number within each team.
    // 0 = player1, 1 = player2
    public int turnNum = 0;

    public int waveNum;


    public string menu = "";

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
                break;

            default:
                UnityEngine.Debug.Log("Invalid Wave");
                break;
        }
    }


    public void nextTurn()
    {
        if (playerTurn)
        {

        }

        else {

            foreach (Spawner spawner in spawners)
            {
                spawner.enemy.selectSkill();
            }
        }

        turnNum++;
        if ((playerTurn && turnNum > players.Count) || (!playerTurn && turnNum > spawners.Count(i => i.isOccupied)))
        {
            turnNum = 0;
            playerTurn = !playerTurn;
        }
    }


}
