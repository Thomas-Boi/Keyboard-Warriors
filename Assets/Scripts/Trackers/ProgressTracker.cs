using System;


// track the game progress as a singleton object
public class ProgressTracker
{
    private int weekNum;
    private SceneTypeEnum curCombatType;

    // the singleton
    private static ProgressTracker tracker;

    public static int finalWeekNum = 2;

    // track the current week number in the semester
    public int WeekNum 
    { 
        get => weekNum; 
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

    // for development purpose only. 
    public bool ProductionMode { get => true; }

    public GamePhase StorylinePhase { get; private set; }

    // track the phase of the game
    public enum GamePhase {
        FirstTime,
        BeforeMidterm,
        AfterMidterm,
        BeforeFinals,
        AfterFinals
    }

    public ProgressTracker() {
        weekNum = 1;
        curCombatType = SceneTypeEnum.HOME;
        StorylinePhase = GamePhase.FirstTime;
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
        checkPhase();
        return weekNum;
    }

    private void checkPhase()
    {
        // goes down and check if we need to change the phase
        int midtermWeek = finalWeekNum / 2;
        if (weekNum == midtermWeek)
        {
            StorylinePhase = GamePhase.BeforeMidterm;
        }
        else if (weekNum == midtermWeek)
        {
            StorylinePhase = GamePhase.BeforeMidterm;
        }
        else if (weekNum == midtermWeek + 1)
        {
            StorylinePhase = GamePhase.AfterMidterm;
        }
        else if (weekNum == finalWeekNum)
        {
            StorylinePhase = GamePhase.BeforeFinals;
        }
        else if (weekNum == finalWeekNum + 1)
        {
            StorylinePhase = GamePhase.AfterFinals;
        }
        else {
            return;
        }
        SceneLoader.LoadStoryScene();
    }
}
