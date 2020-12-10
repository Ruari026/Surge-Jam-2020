using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        TransitionScreenController.instance.FadeIn();
    }

    public void PlayGame()
    {
        // Checks if this is the first load of the game
        if (PersistantData.instance.CheckIfFirstPlay())
        {
            // Play Tutorial
            StartCoroutine(WaitAndPlayGame("Tutorial"));
        }
        else
        {
            // Go Straight To Main Gameplay
            StartCoroutine(WaitAndPlayGame("GamePlay"));
        }
    }

    public void PlayTutorial()
    {
        StartCoroutine(WaitAndPlayGame("Tutorial"));
    }

    private IEnumerator WaitAndPlayGame(string nextScene)
    {
        TransitionScreenController.instance.FadeOut();

        yield return new WaitForSeconds(0.75f);

        SceneManager.LoadScene(nextScene);
    }
}
