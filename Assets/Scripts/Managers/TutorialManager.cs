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
    private float timeToFirstSpawn = 1.0f;
    [SerializeField]
    private int nextSpawningAudience = 0;
    [SerializeField]
    private GameObject[] audienceMemberPrefabs;

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

        QuestionUIManager.OnEventDialogueFinished += SpawnNextTutorialAudience;
        QuestionUIManager.OnEventDialogueFinished += CheckIfEndTutorial;
    }

    private void OnDisable()
    {
        QuestionUIManager.OnEventDialogueFinished -= SpawnNextTutorialAudience;
        QuestionUIManager.OnEventDialogueFinished -= CheckIfEndTutorial;
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
        // Timer for spawning first audience member
        if (!hasSpawned)
        {
            currentSpawnTime += Time.deltaTime;
            if (currentSpawnTime > timeToFirstSpawn)
            {
                hasSpawned = true;
                SpawnNextTutorialAudience();
            }
        }
    }

    private void SpawnNextTutorialAudience()
    {
        if (nextSpawningAudience < audienceMemberPrefabs.Length)
        {
            GameObject nextAudienceMember = audienceMemberPrefabs[nextSpawningAudience];
            AudienceSpawner.instance.SpawnAudienceMember(nextAudienceMember);
        }

        nextSpawningAudience++;
    }

    public void RestartTutorial()
    {
        currentSpawnTime = 0;
        hasSpawned = false;
    }

    public void CheckIfEndTutorial()
    {
        if (nextSpawningAudience >= audienceMemberPrefabs.Length)
        {
            StartCoroutine(DelaySceneChange());
        }
    }
    private IEnumerator DelaySceneChange()
    {
        TransitionScreenController.instance.FadeOut();

        yield return new WaitForSecondsRealtime(0.75f);

        SceneManager.LoadScene("Main_Menu");
    }
}
