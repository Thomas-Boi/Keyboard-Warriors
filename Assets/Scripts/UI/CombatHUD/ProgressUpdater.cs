﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressUpdater : MonoBehaviour
{
    public List<GameObject> screens;
    public Text coins;


    void Awake()
    {
        EventController controller = GameObject.Find("EventController").GetComponent<EventController>();
        Wave wave = controller.weekData.weeks.Find(x => x.weekNum == controller.weekNum).waves[controller.waveNum];

        if (coins != null)
        {
            coins.text = "Coins: " + ItemTracker.GetTracker().Money + " + " + wave.coins + " = " + (ItemTracker.GetTracker().Money + wave.coins);
        }
        for (int i = 0; i < screens.Count; i++)
        {
            Character player = controller.players[i];
            Stats newStats = player.CalcStats(wave.exp);

            foreach (Transform child in screens[i].transform)
            {

                if (child.name == "Name")
                {
                    child.GetComponent<Text>().text = controller.players[i].characterName;
                }
                else if (child.name == "Exp")
                {
                    string text = "EXP Gained: " + wave.exp + "\n";

                    text += "EXP to next level: " + (Character.CalcExpToLevel(newStats.level) - newStats.exp) + " exp";

                    child.GetComponent<Text>().text = text;
                }
                else if (child.name == "Stats")
                {
                    string text = "";
                    if (newStats.level > player.level)
                    {
                        text += "Level: " + player.level + " ---> " + newStats.level + "          LEVEL UP!";
                        text += "\n\nHP: " + player.maxHealth + " + " + newStats.health + "   --> " + (newStats.health + player.maxHealth);
                        text += "\nAttack: " + player.attack + " + " + newStats.attack + "   --> " + (newStats.attack + player.attack);
                        text += "\nDefense: " + player.defense + " + " + newStats.defense + "   --> " + (newStats.defense + player.defense);

                    }
                    else
                    {
                        text += "Level: " + player.level;
                        text += "\n\nHP: " + player.maxHealth;
                        text += "\nAttack: " + player.attack;
                        text += "\nDefense: " + player.defense;
                    }
                    child.GetComponent<Text>().text = text;

                }
                else if (child.name == "Note")
                {
                    // Skill gain messages
                    //todo: Check skills and use their alias instead of hardcoded stirngs
                    string text = "LEARNED NEW SKILL:\n";
                    if (newStats.level > player.level)
                    {
                        if (player.id == "dps")
                        {
                            switch (newStats.level)
                            {
                                case 2:
                                    text += "Heavy Attack";
                                    break;
                                case 4:
                                    text += "Mass Attack";
                                    break;
                                case 5:
                                    text += "Self Study";
                                    break;
                                case 8:
                                    text += "strongWideAttack";
                                    break;
                                default:
                                    text = "";
                                    break;
                            }
                        }
                        else if (player.id == "tank")
                        {
                            switch (newStats.level)
                            {
                                case 2:
                                    text += "Calm";
                                    break;
                                case 4:
                                    text += "Finish Up";
                                    break;
                                case 5:
                                    text += "Motivation";
                                    break;
                                case 7:
                                    text += "Rally";
                                    break;
                                default:
                                    text = "";
                                    break;
                            }

                        }
                        else if (player.id == "support")
                        {
                            switch (newStats.level)
                            {
                                case 2:
                                    text += "Heal";
                                    break;
                                case 4:
                                    text += "Medium Attack";
                                    break;
                                case 6:
                                    text += "Plan Ahead";
                                    break;
                                case 8:
                                    text += "Party Heal";
                                    break;
                                default:
                                    text = "";
                                    break;
                            }
                        }
                    }
                    else
                    {
                        text = "";
                    }
                    child.GetComponent<Text>().text = text;
                }
            }
        }
    }
    // get the rewards for winning then move to the next scene
    // increment the week
    // if it's the final week and they pass, display an end scene
    public void GetWinRewards()
    {
        EventController controller = GameObject.Find("EventController").GetComponent<EventController>();
        Wave wave = controller.weekData.weeks.Find(x => x.weekNum == controller.weekNum).waves[controller.waveNum];


        foreach (Character player in controller.players)
        {
            player.exp += wave.exp;
            player.LevelUp();
        }

        int money = wave.coins;

        UpdateProgress(money);

        List<CharStats> statsList = new List<CharStats>();
        //Update the player's level
        foreach (Character player in controller.players)
        {
            CharStats stats = new CharStats();
            stats.level = player.level;
            stats.exp = player.exp;
            stats.id = player.id;
            statsList.Add(stats);
        }
        ProgressTracker.GetTracker().charStats = statsList;


        if (controller.waveNum != controller.weekData.weeks.Find(x => x.weekNum == controller.weekNum).waves.Count - 1)
        {
            controller.ClearSpawners();
            controller.waveNum++;
            controller.StartWave();
            Object.Destroy(gameObject);
        }
        else
        {
            // see if we need to progress to next week
            var tracker = ProgressTracker.GetTracker();
            if (tracker.CurCombatType == SceneTypeEnum.WEEKLY_COMBAT)
            {
                int curWeek = tracker.WeekNum;
                if (curWeek == ProgressTracker.finalWeekNum)
                {
                    SceneLoader.LoadEndScene();
                    return;
                }
                else tracker.NextWeek();
            }
            SceneLoader.LoadHomeScene();
        }
    }

    // update the player's score and money
    private void UpdateProgress(int money)
    {
        ItemTracker.GetTracker().AddMoney(money);
    }


    // get the rewards for losing then move to the next scene
    public void GetLoseRewards()
    {
        int money = 10;
        UpdateProgress(money);
        SceneLoader.LoadHomeScene();
    }
}
