using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        StartCoroutine(WaitAndPlayGame());
    }

    private IEnumerator WaitAndPlayGame()
    {
        TransitionScreenController.instance.FadeOut();

        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync("Gameplay");
        sceneLoading.allowSceneActivation = false;

        yield return new WaitForSeconds(0.75f);
        while (sceneLoading.progress < 0.9f)
        {
            yield return null;
        }

        sceneLoading.allowSceneActivation = true;
    }
}
