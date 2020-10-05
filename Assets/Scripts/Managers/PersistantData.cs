using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantData : MonoBehaviour
{
    public int score = 0;
    private int hiScore;

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
            Debug.Log("Instance Already Exists");
            Destroy(this.gameObject);
        }
    }

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
}
