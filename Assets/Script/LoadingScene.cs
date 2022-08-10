using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    // Loading Time
    public float LoadingWaitTime;

    // Load Scene index
    public int SceneIndex;

    private void Awake()
    {
        StartCoroutine(SceneChange());
    }

    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(LoadingWaitTime);

        SceneManager.LoadScene(SceneIndex);
    }
}
