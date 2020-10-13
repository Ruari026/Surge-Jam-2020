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



    public void SpawnMarble()
    {
        MarbleController newMarble = Instantiate(marblePrefab, this.transform).GetComponent<MarbleController>();
        newMarble.transform.position = marbleSpawnPos.transform.position;

        newMarble.RandomizeMarbleType();
    }

    public void SpawnMarble(AnswerTypes answerType)
    {
        MarbleController newMarble = Instantiate(marblePrefab, this.transform).GetComponent<MarbleController>();
        newMarble.transform.position = marbleSpawnPos.transform.position;

        newMarble.SetMarbleType(answerType);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(marbleSpawnPos.position, 0.1f);
    }
}
