using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;

    private PlayerController activePlayer;

    public static GameManager instance;

    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        // Check if there is an active GameManager
        // Check if I am the active GameManager
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        // If there is NO active GameManager
        else
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += SpawnPlayerOnLoad;

            // Set the active GameManager as myself
            instance = this;
        }
    }

    void SpawnPlayerOnLoad(Scene currentScene, Scene nextScence)
    {
        // Check if the ActiveScene is at Index 0
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            // IF is 0, do nothing

            //IF is at 0 find Spawn empty script
            //SpawnSpawn spawnSpot = FindObjectOfType<SpawnSpawn>();

            //Move and rotate the Player to the spawnSport
            //activePlayer.transform.position = spawnSpot.transform.position;
            //activePlayer.transform.rotation = spawnSpot.transform.rotation;
            DontDestroyOnLoad(audioSource);
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Audio Is Playing: " + audioSource);
            Destroy(activePlayer);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                StartCoroutine(Wait());
                audioSource.Pause();
                Destroy(activePlayer);
                Destroy(GameObject.Find("Player(Clone)"));
            }
        }
        else
        {
            // Check if there is any active player in the scence
            if (activePlayer == null)
            {
                //If there is no player, I should spawn one
                GameObject newPlayer = Instantiate(playerPrefab, new Vector3(-28,21,0), Quaternion.identity);
                // Store the new Player
                activePlayer = newPlayer.GetComponent<PlayerController>();
                DontDestroyOnLoad(newPlayer);
            }
            else
            {
                // If there is already a player, don't do anything for now
                SpawnSpawn spawnSpot = FindObjectOfType<SpawnSpawn>();

                // Move and rotate the Player to the spawnSpot
                activePlayer.transform.position = spawnSpot.transform.position;
                activePlayer.transform.rotation = spawnSpot.transform.rotation;
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(2);
    }
}
