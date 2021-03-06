﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Singleton Design Pattern
    private static LevelManager _Instance;
    public static LevelManager instance
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

    [Header("Player Variations")]
    [SerializeField]
    private GameObject[] possiblePlayerCharacters;

    [Header("Audience Spawning")]
    private float currentSpawnTime = 0;
    private float currentLevelTime = 0;
    [SerializeField]
    private float baseSpawnTime = 5.0f;
    [SerializeField]
    private float maxSpawnReduction = 4.5f;
    [SerializeField]
    private float timeToNextSpawn = 1.0f;
    [SerializeField]
    private AnimationCurve spawnTimeCurve;

    [Header("Platform Handling")]
    [SerializeField]
    private PlatformController thePlatform;
    [SerializeField]
    private float instabilityIncreaseRate = 2.0f;

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

        MarbleController.OnEventMarbleOutOfBounds += EndGame;
    }

    private void OnDisable()
    {
        MarbleController.OnEventMarbleOutOfBounds -= EndGame;
    }

    // Start is called before the first frame update
    void Start()
    {
        PersistantData.instance.ResetScore();

        int pickedPlayerCharacter = Random.Range(0, possiblePlayerCharacters.Length);
        for (int i = 0; i < possiblePlayerCharacters.Length; i++)
        {
            if (i == pickedPlayerCharacter)
            {
                possiblePlayerCharacters[i].SetActive(true);
            }
            else
            {
                possiblePlayerCharacters[i].SetActive(false);
            }
        }

        TransitionScreenController.instance.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        currentSpawnTime += Time.deltaTime;
        if (currentSpawnTime > timeToNextSpawn)
        {
            currentSpawnTime = 0;
            timeToNextSpawn = baseSpawnTime - spawnTimeCurve.Evaluate((float)PersistantData.instance.totalMarbles * 0.1f) * maxSpawnReduction;

            AudienceSpawner.instance.SpawnAudienceMember();
        }
    }

    public void IncreaseInstability()
    {
        thePlatform.instabilityAmount *= instabilityIncreaseRate;
    }

    public void EndGame(MarbleController marble)
    {
        SceneManager.LoadScene("Game_Over");
    }
}
