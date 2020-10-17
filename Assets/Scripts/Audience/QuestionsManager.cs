using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionsManager : MonoBehaviour
{
    // Singleton Design Pattern
    private static QuestionsManager _Instance;
    public static QuestionsManager instance
    {
        get
        {
            if (_Instance != null)
            {
                return _Instance;
            }
            else
            {
                Debug.LogError("ERROR: No Instance Exists");
                return null;
            }
        }
    }

    [SerializeField]
    private List<QuestionAnswersScriptableObject> initalQuestionSet;
    private List<QuestionAnswersScriptableObject> reuseableQuestionSet;
    private bool questionsFinished = false;

    private void OnEnable()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Debug.LogError("ERROR: Instance Already Exists");
        }
    }

    public QuestionAnswersScriptableObject GetQuestion()
    {
        QuestionAnswersScriptableObject newQuestion = null;

        if (questionsFinished)
        {
            // All questions have been given at least once
            int pickedQuestion = Random.Range(0, reuseableQuestionSet.Count);
            newQuestion = reuseableQuestionSet[pickedQuestion];
        }
        else
        {
            // Pick from the remaining inital set of questions
            int pickedQuestion = Random.Range(0, initalQuestionSet.Count);
            newQuestion = initalQuestionSet[pickedQuestion];

            // Remove picked question from inital set & add to reusable set
            initalQuestionSet.Remove(newQuestion);
            reuseableQuestionSet.Add(newQuestion);

            // checking if inital questions are finished
            if (initalQuestionSet.Count == 0)
            {
                questionsFinished = true;
            }
        }

        return newQuestion;
    }
}
