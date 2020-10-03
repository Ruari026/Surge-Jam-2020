using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceSpawner : MonoBehaviour
{
    // Singleton Design Pattern
    private static AudienceSpawner _Instance;
    public static AudienceSpawner instance
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

    [Header("Audience Handling")]
    [SerializeField]
    private GameObject audienceMemberPrefab;

    [Header("Spawn Bounds")]
    public Transform[] spawnRows;
    public float[] rowWidths;

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

    public void SpawnAudienceMember()
    {
        GameObject newAudienceMember = Instantiate(audienceMemberPrefab, this.transform);

        Vector3 distance = Vector3.zero;

        // Depth Position
        int pickedRow = Random.Range(0, spawnRows.Length);
        distance = spawnRows[pickedRow].position;

        // Horizontal Position
        float range = rowWidths[pickedRow];
        distance.x = Random.Range(-range, range);

        newAudienceMember.transform.position = distance;
        newAudienceMember.transform.rotation = Quaternion.identity;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < spawnRows.Length; i++)
        {
            Debug.DrawLine(spawnRows[i].position, spawnRows[i].position + (Vector3.right * rowWidths[i]));
            Debug.DrawLine(spawnRows[i].position, spawnRows[i].position - (Vector3.right * rowWidths[i]));
        }
    }
}
