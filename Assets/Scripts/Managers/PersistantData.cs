using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantData : MonoBehaviour
{
    // General HiScore
    public int totalMarbles = 0;
    private int hiScore;
    // Storing Each Marble Type
    private Dictionary<AnswerTypes, int> eachMarbles;

    // Tutorial Handling
    [SerializeField]
    private int firstLoad = 0;

    // Singleton Design Pattern
    private static PersistantData _Instance;
    public static PersistantData instance
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

    private void OnEnable()
    {
        if (_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.Log("Instance Already Exists, Destroying This...");
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        firstLoad = PlayerPrefs.GetInt("FirstLoad");
    }


    /*
    ============================================================================================================================================================================================================================================================================================================
    Game Tutorial Handling
    ============================================================================================================================================================================================================================================================================================================
    */
    public bool CheckIfFirstPlay()
    {
        return firstLoad == 0 ? 
            true : 
            false;
    }

    public void FinishedTutorial()
    {
        firstLoad = 1;
        PlayerPrefs.SetInt("FirstLoad", 1);
    }

    /*
    ============================================================================================================================================================================================================================================================================================================
    Game Score Handling
    ============================================================================================================================================================================================================================================================================================================
    */
    public void ResetScore()
    {
        // Resetting 
        totalMarbles = 0;
        eachMarbles = new Dictionary<AnswerTypes, int>();

        // Setting Up Dictionary
        eachMarbles.Add(AnswerTypes.RELATIONSHIPS, 0);
        eachMarbles.Add(AnswerTypes.EDUCATION, 0);
        eachMarbles.Add(AnswerTypes.ECONOMY, 0);
        eachMarbles.Add(AnswerTypes.NATURE, 0);
        eachMarbles.Add(AnswerTypes.HUMANITY, 0);
        eachMarbles.Add(AnswerTypes.ARTS, 0);
    }

    public void AddScore()
    {
        totalMarbles++;
    }

    public void AddScore(AnswerTypes answerType)
    {
        totalMarbles++;
        eachMarbles[answerType]++;
    }


    /*
    ============================================================================================================================================================================================================================================================================================================
    Hi Score Handling
    ============================================================================================================================================================================================================================================================================================================
    */
    public int GetHighScore()
    {
        hiScore = PlayerPrefs.GetInt("HiScore");
        return hiScore;
    }

    public void SetHighScore(int newScore)
    {
        hiScore = PlayerPrefs.GetInt("HiScore");
        if (newScore > hiScore)
        {
            hiScore = newScore;
            PlayerPrefs.SetInt("HiScore", newScore);
        }
    }

    public static void ResetHiScore()
    {
        PlayerPrefs.SetInt("HiScore", 0);
    }


    /*
    ============================================================================================================================================================================================================================================================================================================
    Marble Type Analysis
    ============================================================================================================================================================================================================================================================================================================
    */
    public Dictionary<AnswerTypes, int> GetAllPickedMarbles()
    {
        return eachMarbles;
    }

    public AnswerTypes GetMostPickedAnswerType()
    {
        AnswerTypes a = GetMostPickedAnswerType(out float p);
        return a;
    }

    public AnswerTypes GetMostPickedAnswerType(out float answerPercentage)
    {
        AnswerTypes answerType = AnswerTypes.RELATIONSHIPS;
        if (eachMarbles == null)
            ResetScore();
        int answerAmount = eachMarbles[answerType];

        foreach (KeyValuePair<AnswerTypes, int> pair in eachMarbles)
        {
            if (pair.Value > answerAmount)
            {
                answerType = pair.Key;
                answerAmount = pair.Value;
            }
        }

        float percentage = (float)answerAmount / totalMarbles;
        answerPercentage = percentage;

        return answerType;
    }
}
