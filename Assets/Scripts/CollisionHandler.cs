using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandller : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    bool isTransitioning = false;
    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) {return;}

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Looking good!");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            case "Fuel":
                Debug.Log("Whoop Whoop!");
                break;
            case "Ground":
                StartCrashSequence();
                break;
            default:
                Debug.Log("Booooooom!");
                break;
        }
    }

    private void StartCrashSequence()
    {
        GetComponent<RocketMovement>().enabled = false;
        isTransitioning = true;
        StartCoroutine(ReloadLevelAfterDelay());
    }

    private IEnumerator ReloadLevelAfterDelay()
    {
        yield return new WaitForSeconds(levelLoadDelay);
        int currenSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currenSceneIndex);
    }

    private void StartSuccessSequence()
    {
        isTransitioning = false;
        StartCoroutine(LoadNextLevelAfterDelay());
    }

    private IEnumerator LoadNextLevelAfterDelay()
    {
        yield return new WaitForSeconds(levelLoadDelay);
        int currenSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currenSceneIndex + 1 ) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    private void LoadNextLevel()
    {
        int currenSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currenSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);

    }
}
