using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject noSavedGameDialog = null;

    // Audio
    public AudioClip BGM;
    public AudioSource audioSource;

    private void Awake()
    {   
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(BGM);
    }

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
