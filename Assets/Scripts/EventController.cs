using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public List<Spawner> spawners;


    public List<Character> players;

    // true = player turn
    // false = not player turn
    public bool playerTurn = false;

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
    }

}
