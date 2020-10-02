using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public Spawner spawn1;
    public Spawner spawn2;
    public Spawner spawn3;

    // turn 0 = Player Team
    // turn 1 = Enemy Team
    // turn ??? = Maybe damage from like sandstorm or something idk
    public int turn = 1; 

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
                spawn1.spawn("boxSlime");
                spawn2.spawn("boxSlimeSmall");
                spawn3.spawn("boxSlimeSmall");
                break;

            default:
                UnityEngine.Debug.Log("Invalid Wave");
                break;
        }
    }

}
