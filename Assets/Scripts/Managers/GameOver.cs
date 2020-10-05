using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private Text hiscoreText;
    [SerializeField]
    private Text finalScoreText;

    private void Start()
    {
        PersistantData persistantData = PersistantData.instance;
        persistantData.SetHighScore(persistantData.score);

        hiscoreText.text = persistantData.GetHighScore().ToString() + "- Marbles";
        finalScoreText.text = persistantData.score.ToString() + "- Marbles";
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
