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

    // Reference to the UI buttons
    public Button skillButton;
    public Button tacticsButton;
    public Button itemButton;

    // references to menu prefabs
    public GameObject tacticsMenuPrefab;

    // references to menus
    private GameObject tacticsMenu;


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
    

    // Will bring up the skills sub-menu
    private void UseSkills()
    {
        Debug.Log("Skills");
        // tacticsMenu.GetComponent<Renderer>().enabled = false;
    }

    // Will bring up the tactics sub-menu
    public void UseTactics()
    {
        Debug.Log("Tactics");
        //if (tacticsMenu == null)
        //{
        //    tacticsMenu = Instantiate(tacticsMenuPrefab, transform);
        //}
        //else
        //{
        //    tacticsMenu.GetComponent<Renderer>().enabled = true;
        //}
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

}
