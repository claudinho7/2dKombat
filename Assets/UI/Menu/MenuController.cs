using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void MultiPlayer()
    {
        SceneManager.LoadScene("Scenes/MPScene", LoadSceneMode.Single);
        Debug.Log("Loading Multiplayer scene");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void NextRoundMP()
    {
        SceneManager.LoadScene("Scenes/MPScene", LoadSceneMode.Single);
    }

    public void LeaveGame()
    {
        SceneManager.LoadScene("Scenes/MenuScene", LoadSceneMode.Single);
        CombatSystem.p1lifes = 3;
        P2CombatSystem.p2lifes = 3;
    }
    
    
}
