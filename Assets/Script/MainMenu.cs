using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // New Game Scene Index
    public int NewGameSceneIndex;

    public void NewGame()
    {
        SceneManager.LoadScene(NewGameSceneIndex);
    }
}
