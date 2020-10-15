using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Checks if this is the first load of the game
        if (PersistantData.instance.CheckIfFirstPlay())
        {
            // Play Tutorial
            Debug.Log("Needs To Play Tutorial");
        }
        else
        {
            // Go Straight To Main Gameplay
        }

        StartCoroutine(WaitAndPlayGame("GamePlay"));
    }

    private IEnumerator WaitAndPlayGame(string nextScene)
    {
        TransitionScreenController.instance.FadeOut();

        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(nextScene);
        sceneLoading.allowSceneActivation = false;

        yield return new WaitForSeconds(0.75f);
        while (sceneLoading.progress < 0.9f)
        {
            yield return null;
        }

        sceneLoading.allowSceneActivation = true;
    }
}
