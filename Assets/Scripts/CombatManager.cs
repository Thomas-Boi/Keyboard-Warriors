using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{

    private EventController controller;

    // reference to the playable character
    private Character player;

    // Reference to the UI buttons
    public Button attackButton;
    public Button skillButton;
    public Button tacticsButton;
    public Button itemButton;

    public HealthBar p1HealthBar;

    void Awake()
    {
        player = GameObject.Find("Character").GetComponent<Character>();
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

    // Update is called once per frame
    void Update()
    {
        
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


        controller.nextTurn();
    }

    // For player to select target
    private IEnumerator StartEnemyTarget()
    {
        bool targetting = true;
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
                    if (target.isEnemy)
                    {
                        //Debug.Log(target.name);
                        player.useSkill("basicAttack", target);
                        targetting = false;
                    }
                }

            }

            yield return new WaitForEndOfFrame();
            
        }
        
    }
}
