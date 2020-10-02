using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public float level;
    public float health;
    public float mana;
    public float attack;
    public float defense;
    public bool isEnemy = true;



    void OnMouseEnter()
    {
        transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        transform.position = transform.position + new Vector3(0, 0.3f, 0);

    }

    void OnMouseOver()
    {
        //transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        
    }

    // ...and the mesh finally turns white when the mouse moves away.
    void OnMouseExit()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.position = transform.position + new Vector3(0, -0.3f, 0);
    }
}
