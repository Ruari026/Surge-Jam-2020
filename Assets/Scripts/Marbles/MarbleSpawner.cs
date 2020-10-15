using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleSpawner : MonoBehaviour
{
    // Singleton Design Pattern
    private static MarbleSpawner _Instance;
    public static MarbleSpawner instance
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

    [Header("Spawn Range Handling")]
    [SerializeField]
    private Transform minSpawnPos;
    [SerializeField]
    private Transform maxSpawnPos;

    [Header("Marble Details")]
    [SerializeField]
    private GameObject marblePrefab;

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



    public void SpawnMarble()
    {
        Vector3 spawnPos = new Vector3
        {
            x = Random.Range(minSpawnPos.position.x, maxSpawnPos.position.x),
            y = minSpawnPos.position.y,
            z = minSpawnPos.position.z
        };

        // Spawning New Marble
        MarbleController newMarble = Instantiate(marblePrefab, this.transform).GetComponent<MarbleController>();
        newMarble.transform.position = spawnPos;

        newMarble.RandomizeMarbleType();
    }

    public void SpawnMarble(AnswerTypes answerType)
    {
        Vector3 spawnPos = new Vector3
        {
            x = Random.Range(minSpawnPos.position.x, maxSpawnPos.position.x),
            y = minSpawnPos.position.y,
            z = minSpawnPos.position.z
        };

        MarbleController newMarble = Instantiate(marblePrefab, this.transform).GetComponent<MarbleController>();
        newMarble.transform.position = spawnPos;

        newMarble.SetMarbleType(answerType);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Gizmos.DrawWireSphere(minSpawnPos.position, 0.1f);
        Gizmos.DrawWireSphere(maxSpawnPos.position, 0.1f);

        Gizmos.DrawLine(minSpawnPos.position, maxSpawnPos.position);
    }
}
