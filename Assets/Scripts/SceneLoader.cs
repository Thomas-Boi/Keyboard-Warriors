using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    static string COMBAT_SCENE_NAME = "CombatScene";
    static string HOME_SCENE_NAME = "HomeScene";

    // load the combat scene
    public static void LoadCombatScene() 
    {
        SceneManager.LoadScene(COMBAT_SCENE_NAME);
    }

    // load the Home scene
    public static void LoadHomeScene()
    {
        SceneManager.LoadScene(HOME_SCENE_NAME);
    }

}
