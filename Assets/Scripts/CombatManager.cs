using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{

    private EventController controller;

    // reference to the playable characters
    public Character player;
    public Character player2;

    // TODO: on certain turn number switch current controlled player over to the next one
    private Character currentPlayer;

    // Reference to the UI buttons
    
    public Button skillButton;
    public Button tacticsButton;
    public Button itemButton;




    void Awake()
    {
        controller = GameObject.Find("EventController").GetComponent<EventController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //attackButton.onClick.AddListener(() => UseAttack());
        skillButton.onClick.AddListener(() => UseSkills());
        tacticsButton.onClick.AddListener(() => UseTactics());
        itemButton.onClick.AddListener(() => UseItems());
    }

    void Update()
    {


    }

    // Player's basic attack
    

    // Will bring up the skills sub-menu
    private void UseSkills()
    {
        Debug.Log("Skills");
    }

    // Will bring up the tactics sub-menu
    public void UseTactics()
    {
        Debug.Log("Tactics");
    }

    // For the time being, uses only a single item that heals the player for a set amount
    public void UseItems()
    {
        Debug.Log("Healing Item");
        // set amount of health back
        float health = player.health;
        health += 15;
        player.SetCharacterHealth(health);

        controller.nextTurn();
    }

    // For player to select target
    //private IEnumerator StartEnemyTarget()
    //{
    //    bool targetting = true;
    //    foreach (Spawner spawn in controller.spawners)
    //    {
    //        if (spawn.isOccupied)
    //        {
    //            spawn.enemy.isTargetable = true;
    //        }
    //    }

    //    while (targetting)
    //    {

    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //            RaycastHit hit;
    //            Character target;

    //            if (Physics.Raycast(ray, out hit, 100))
    //            {
    //                target = hit.transform.gameObject.GetComponent<Character>();
    //                if (target && target.isEnemy && target.parent.isOccupied)
    //                {
    //                    Debug.Log(target.name);
    //                    StartCoroutine(player.useSkill("basicAttack", target));

    //                    // when attacking, player's stress increases
    //                    float stress = player.stress;
    //                    stress += 20;
    //                    player.SetCharacterStress(stress);

    //                    foreach (Spawner spawn in controller.spawners)
    //                    {
    //                        if (spawn.isOccupied)
    //                        {
    //                            spawn.enemy.isTargetable = false;
    //                            spawn.enemy.resetScale();
    //                        }
    //                    }
    //                    targetting = false;
    //                }
    //            }

    //        }

    //        yield return null;
            
    //    }
        
    //}
}
