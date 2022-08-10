using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseController : MonoBehaviour
{
    public static bool pause = false;
    public GameObject menu;

    PauseInputs actions;

    private void Awake()
    {
        actions = new PauseInputs();
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    private void Start()
    {
        // Using Input System KeyMap to call function DeterminePause
        actions.Pause.PauseKey.performed += _ => DeterminePause();
    }

    private void DeterminePause()
    {
        if (pause)
        {
            Resume();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pause = true;
        menu.SetActive(true);
        AudioListener.pause = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pause = false;
        menu.SetActive(false);
        AudioListener.pause = false;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
