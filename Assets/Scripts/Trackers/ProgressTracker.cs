using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// track the game progress as a singleton object
public class ProgressTracker
{
    private int weekNum;
    private int score;
    private SceneTypeEnum curCombatType;


    // the singleton
    private static ProgressTracker tracker;

    public static int finalWeekNum = 2;

    // track the current week number in the semester
    public int WeekNum 
    { 
        get => weekNum; 
    }

    // track the player's score
    public int Score
    { 
        get => score; 
    }

    // track the combat type
    public SceneTypeEnum CurCombatType 
    {
        get => curCombatType;
        set
        {
            if (value == SceneTypeEnum.WEEKLY_COMBAT || value == SceneTypeEnum.EXTRA_COMBAT)
            {
                curCombatType = value;
            }
        }
    }


    public ProgressTracker() {
        weekNum = 1;
        score = 0;
        curCombatType = SceneTypeEnum.HOME;
    }

    public static ProgressTracker GetTracker()
    {
        if (tracker == null) tracker = new ProgressTracker();
        return tracker;
    }

    // go to the next week but it can't be more than finalWeekNum
    // return the value of the next week
    public int NextWeek()
    {
        if (++weekNum > finalWeekNum)
        {
            weekNum = finalWeekNum;
        }
        return weekNum;
    }

    // gain score for the player
    public void AddScore(int amount)
    {
        if (amount > 0) score += amount;
    }
}
