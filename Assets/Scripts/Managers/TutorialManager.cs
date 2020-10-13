using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    // Singleton Design Pattern
    private static TutorialManager _Instance;
    public static TutorialManager instance
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
    private bool hasSpawned = false;
    [SerializeField]
    private float timeToNextSpawn = 1.0f;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasSpawned)
        {
            currentSpawnTime += Time.deltaTime;

            if (currentSpawnTime > timeToNextSpawn)
            {
                AudienceSpawner.instance.SpawnAudienceMember();
                hasSpawned = true;
            }
        }
    }

    public void RestartTutorial()
    {
        currentSpawnTime = 0;
        hasSpawned = false;
    }

    public void EndTutorial()
    {
        SceneManager.LoadScene("Game_Over");
    }
}
