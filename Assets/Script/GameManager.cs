using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;

    private PlayerController activePlayer;

    public static GameManager instance;

    private void Awake()
    {
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
            SpawnSpawn spawnSpot = FindObjectOfType<SpawnSpawn>();

            //Move and rotate the Player to the spawnSport
            activePlayer.transform.position = spawnSpot.transform.position;
            activePlayer.transform.rotation = spawnSpot.transform.rotation;
        }
        else
        {
            // Check if there is any active player in the scence
            if (activePlayer == null)
            {
                //If there is no player, I should spawn one
                GameObject newPlayer = Instantiate(playerPrefab, new Vector3(0,20,0), Quaternion.identity);
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
}
