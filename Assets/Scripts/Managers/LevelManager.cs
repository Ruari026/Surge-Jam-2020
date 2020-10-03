using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Handling GameState

    // Handling Audience
    [SerializeField]
    private GameObject audiencePrefab;
    private float currentSpawnTime = 0;
    private float timeToNextSpawn = 1.0f;

    // Handling Marbles
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
        
    }

    // Update is called once per frame
    void Update()
    {
        currentSpawnTime += Time.deltaTime;
        if (currentSpawnTime > timeToNextSpawn)
        {
            SpawnAudienceMember();
        }
    }

    private void SpawnAudienceMember()
    {

    }

    public void SpawnMarble()
    {
        GameObject newMarble = Instantiate(marblePrefab, this.transform);
        newMarble.transform.position = marbleSpawnPos.transform.position;
    }
}
