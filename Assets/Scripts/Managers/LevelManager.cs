using System.Collections;
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

    [Header("Marble Spawning")]
    [SerializeField]
    private GameObject marblePrefab;
    [SerializeField]
    private Transform marbleSpawnPos;

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
        PersistantData.instance.score = 0;

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
        currentSpawnTime += Time.deltaTime;
        if (currentSpawnTime > timeToNextSpawn)
        {
            currentSpawnTime = 0;
            timeToNextSpawn = baseSpawnTime - spawnTimeCurve.Evaluate((float)PersistantData.instance.score * 0.1f) * maxSpawnReduction;

            AudienceSpawner.instance.SpawnAudienceMember();
        }
    }

    public void SpawnMarble()
    {
        MarbleController newMarble = Instantiate(marblePrefab, this.transform).GetComponent<MarbleController>();
        newMarble.transform.position = marbleSpawnPos.transform.position;

        newMarble.RandomizeMarbleType();

        PersistantData.instance.score++;
    }

    public void SpawnMarble(AnswerTypes answerType)
    {
        MarbleController newMarble = Instantiate(marblePrefab, this.transform).GetComponent<MarbleController>();
        newMarble.transform.position = marbleSpawnPos.transform.position;

        newMarble.SetMarbleType(answerType);

        PersistantData.instance.score++;
    }

    public void IncreaseInstability()
    {
        thePlatform.instabilityAmount *= instabilityIncreaseRate;
    }

    public void EndGame()
    {
        SceneManager.LoadScene("Game_Over");
    }
}
