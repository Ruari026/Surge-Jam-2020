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
        persistantData.SetHighScore(persistantData.totalMarbles);

        // Handling HiScore
        hiscoreText.text = persistantData.GetHighScore().ToString() + "- Marbles";
        finalScoreText.text = persistantData.totalMarbles.ToString() + "- Marbles";

        // Handling Marble Type Analysis
        AnswerTypes mostPicked = persistantData.GetMostPickedAnswerType(out float answerPercentage);
        switch (mostPicked)
        {
            case AnswerTypes.RELATIONSHIPS:
                break;

            case AnswerTypes.EDUCATION:
                break;

            case AnswerTypes.ECONOMY:
                break;

            case AnswerTypes.NATURE:
                break;

            case AnswerTypes.HUMANITY:
                break;

            case AnswerTypes.ARTS:
                break;
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
