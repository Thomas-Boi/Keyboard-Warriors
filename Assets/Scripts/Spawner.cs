using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool isOccupied = false;
    public Character enemy;

    public void spawn(string prefab)
    {
        if (!isOccupied)
        {
            GameObject go = Instantiate(Resources.Load("Characters/" + prefab), transform.position, transform.rotation) as GameObject;
            enemy = go.GetComponent<Character>();
            enemy.parent = this;
            isOccupied = true;
        }
        else
        {
            UnityEngine.Debug.Log("Enemy still alive");
        }
    }

    public void kill()
    {
        if (isOccupied)
        {
            isOccupied = false;
        }
    }


}
