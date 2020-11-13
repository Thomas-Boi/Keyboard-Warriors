using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    static string COMBAT_SCENE_NAME = "CombatScene";
    static string HOME_SCENE_NAME = "HomeScene";
    static string MENU_SCENE_NAME = "MenuScene";
    static string END_SCENE_NAME = "EndScene";
    static string ITEM_SHOP_SCENE_NAME = "ItemShopScene";
    static string STORY_SCENE_NAME = "StoryScene";


    // load the combat scene
    // combatType is an int value that corresponds to a value in the CombatTypeEnum
    public static void LoadCombatScene(int combatType) 
    {
        var combatTypeEnum = (SceneTypeEnum)combatType;
        SceneManager.LoadScene(COMBAT_SCENE_NAME);
        ProgressTracker.GetTracker().CurCombatType = combatTypeEnum;
    }

    // load the Home scene
    public static void LoadHomeScene()
    {
        SceneManager.LoadScene(HOME_SCENE_NAME);
    }

    // load the Menu scene
    public static void LoadMenuScene()
    {
        SceneManager.LoadScene(MENU_SCENE_NAME);
    }

    // load the End scene
    public static void LoadEndScene()
    {
        SceneManager.LoadScene(END_SCENE_NAME);
    }

    public static void LoadShopScene()
    {
        SceneManager.LoadScene(ITEM_SHOP_SCENE_NAME);
    }

    public static void LoadStoryScene()
    {
        SceneManager.LoadScene(STORY_SCENE_NAME);
    }

}
