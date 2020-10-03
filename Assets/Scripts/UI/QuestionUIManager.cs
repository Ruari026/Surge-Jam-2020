﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionUIManager : MonoBehaviour
{
    private static QuestionUIManager _Instance;
    public static QuestionUIManager instance
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
    private Text questionText;
    [SerializeField]
    private Text[] answerTexts;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseQuestionUI(bool questionAnswered)
    {
        if (questionAnswered)
        {
            // Add a marble
            LevelManager.instance.SpawnMarble();
        }
        else
        {
            // Increase platform instability
        }

        // Close UI
    }

    public void SetUIFields(QuestionAnswersScriptableObject set)
    {
        questionText.text = set.question;

        for (int i = 0; i < answerTexts.Length; i++)
        {
            answerTexts[i].text = set.answers[i];
        }
    }
}
