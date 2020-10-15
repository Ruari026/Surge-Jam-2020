using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [Header("HiScore Handling")]
    [SerializeField]
    private Text hiscoreText;
    [SerializeField]
    private Text finalScoreText;

    [Header("Facts Handling")]
    [SerializeField]
    private float timeBetweenFacts;
    private float currentFactTime;
    private int currentShownFact = 0;
    [SerializeField]
    public GameObject[] theFacts;

    [Header("Background Marbles Handling")]
    [SerializeField]
    private Queue<AnswerTypes> marblesToSpawn;
    [SerializeField]
    private float timeBetweenSpawns;
    private float currentSpawnTime = 0;

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
        SetupMarbleStack();
    }

    private void Update()
    {
        RunGameOverFactsAnim();
        RunMarbleRunAnalysis();
    }


    /*
    ========================================================================================================================================================================================================
    Marble Analysis Handling
    ========================================================================================================================================================================================================
    */
    private void RunGameOverFactsAnim()
    {
        currentFactTime -= Time.deltaTime;
        if (currentFactTime <= 0)
        {
            GameObject current = theFacts[currentShownFact];

            currentShownFact++;
            if (currentShownFact >= theFacts.Length)
                currentShownFact = 0;

            GameObject next = theFacts[currentShownFact];

            StartCoroutine(ChangeBetweenFacts(current, next));

            currentFactTime = timeBetweenFacts;
        }
    }

    private IEnumerator ChangeBetweenFacts(GameObject currentShownFact, GameObject nextShownFact)
    {
        // Hiding Current Fact
        Animator currentAnimController = currentShownFact.GetComponent<Animator>();
        currentAnimController.SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        currentShownFact.SetActive(false);

        // Showing Next Fact
        nextShownFact.SetActive(true);
        Animator nextAnimController = nextShownFact.GetComponent<Animator>();
        nextAnimController.SetTrigger("Open");
    }


    /*
    ========================================================================================================================================================================================================
    Marble Run Handling
    ========================================================================================================================================================================================================
    */
    private void SetupMarbleStack()
    {
        PersistantData persistantData = PersistantData.instance;

        List<AnswerTypes> orderedList = new List<AnswerTypes>();
        foreach (KeyValuePair<AnswerTypes, int> pair in persistantData.GetAllPickedMarbles())
        {
            for (int i = 0; i < pair.Value; i++)
            {
                orderedList.Add(pair.Key);
            }
        }
        List<AnswerTypes> shuffledList = orderedList.Shuffle();

        marblesToSpawn = new Queue<AnswerTypes>();
        foreach (AnswerTypes a in shuffledList)
        {
            marblesToSpawn.Enqueue(a);
        }
    }

    private void RunMarbleRunAnalysis()
    {
        currentSpawnTime += Time.deltaTime;
        if (currentSpawnTime >= timeBetweenSpawns)
        {
            if (marblesToSpawn.Count > 0)
            {
                AnswerTypes newMarble = marblesToSpawn.Dequeue();
                MarbleSpawner.instance.SpawnMarble(newMarble);
            }
            currentSpawnTime = 0;
        }
    }


    /*
    ========================================================================================================================================================================================================
    Scene Movement
    ========================================================================================================================================================================================================
    */
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
