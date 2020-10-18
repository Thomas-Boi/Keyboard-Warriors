using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Character's stats
    public string characterName;
    public int level;
    public int maxHealth;
    public int health;
    public int maxStress;
    public int stress;
    public int attack;
    public int defense;
    public string ai;

    public bool isTargetable = false;
    public bool isEnemy = true;

    private SkillManager skillManager;
    private EventController controller;
    public GameObject marker;
    public Spawner parent;

    // Reference to character's health and stress bars
    public HealthBar healthBar;
    public StressBar stressBar;

    void Awake()
    {
        controller = GameObject.Find("EventController").GetComponent<EventController>();
        skillManager = GameObject.Find("EventController").GetComponent<SkillManager>();
    }

    void Start()
    {

        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(health);

        if (!isEnemy)
        {
            stressBar.SetMaxStress(maxStress);
            stressBar.SetStress(stress);
        }
    }


    void OnMouseEnter()
    {

        if (isTargetable)
        {
            transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            transform.position = transform.position + new Vector3(0, 0.3f, 0);
        }
        

    }


    // ...and the mesh finally turns white when the mouse moves away.
    void OnMouseExit()
    {
        if (isTargetable)
        {
            resetScale();
        }
        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && isTargetable)
        {

            resetScale();
            StartCoroutine(skillManager.useSkill(controller.players[controller.turnNum], this, controller.selectedSkill));
            isTargetable = false;
        }
    }


    public void resetScale()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.position = parent.transform.position;
    }

    // Set this character's current health
    public void SetCharacterHealth(int health)
    {
        healthBar.SetHealth(health);
        this.health = health;
    }

    // Set this character's current stress
    public void SetCharacterStress(int stress)
    {
        stressBar.SetStress(stress);
        this.stress = stress;
    }

    public void DisplayTurnMarker(bool enabled)
    {
        if (!isEnemy)
        {
            marker.SetActive(enabled);
        }
    }

    public IEnumerator waitForAnim(string animation)
    {
        GetComponent<Animator>().Play(animation, 0, 0);
        yield return new WaitForSeconds(1);
    }


    // Transfered code over from CombatManager
    // todo: Items
    public IEnumerator UseItem(Character user, Character target, string item)
    {
        int healAmount = 15;
        int currentHealth = user.health;
        user.SetCharacterHealth(currentHealth + healAmount);

        yield return new WaitForSeconds(0.5f);
    }

    // todo: Tactics
    public IEnumerator UseTactic(Character user, Character target, string tactic)
    {
        yield return null;
    }
}
