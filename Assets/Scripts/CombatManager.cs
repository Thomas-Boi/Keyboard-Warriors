using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{

    private EventController controller;

    // reference to the playable character
    private Character player;
    //private Character player2;
    //private Character player3;

    // Reference to the UI buttons
    public Button attackButton;
    public Button skillButton;
    public Button tacticsButton;
    public Button itemButton;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Character>();
        controller = GameObject.Find("EventController").GetComponent<EventController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        attackButton.onClick.AddListener(() => UseAttack());
        skillButton.onClick.AddListener(() => UseSkills());
        tacticsButton.onClick.AddListener(() => UseTactics());
        itemButton.onClick.AddListener(() => UseItems());
    }

    void Update()
    {
        if (player.health <= 0) {
            player.health = 0;

            // display text to say you lost battle
        }
    }

    // Player's basic attack
    private void UseAttack()
    {
        Debug.Log("Attack");
        StartCoroutine(StartEnemyTarget());
    }

    // Will bring up the skills sub-menu
    private void UseSkills()
    {
        Debug.Log("Skills");
        //controller.nextTurn();
    }

    // Will bring up the tactics sub-menu
    public void UseTactics()
    {
        Debug.Log("Tactics");
        //controller.nextTurn();
    }

    // For the time being, uses only a single item that heals the player for a set amount
    public void UseItems()
    {
        Debug.Log("Healing Item");
        // set amount of health back
        float heal = player.health + 8;
        player.SetCharacterHealth(heal);

        controller.nextTurn();
    }

    // For player to select target
    private IEnumerator StartEnemyTarget()
    {
        bool targetting = true;
        foreach (Spawner spawn in controller.spawners)
        {
            if (spawn.isOccupied)
            {
                spawn.enemy.isTargetable = true;
            }
        }

        while (targetting)
        {

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Character target;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    target = hit.transform.gameObject.GetComponent<Character>();
                    if (target.isEnemy && target.parent.isOccupied)
                    {
                        Debug.Log(target.name);
                        StartCoroutine(player.useSkill("basicAttack", target));
                        foreach (Spawner spawn in controller.spawners)
                        {
                            if (spawn.isOccupied)
                            {
                                spawn.enemy.isTargetable = false;
                                spawn.enemy.resetScale();
                            }
                        }
                        targetting = false;
                    }
                }

            }

            yield return null;
            
        }
        
    }
}
