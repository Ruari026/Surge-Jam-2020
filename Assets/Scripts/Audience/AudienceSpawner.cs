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
    [SerializeField]
    private List<SpawnPoint> allSpawnPoints;
    private List<SpawnPoint> availableSpawnPoints;
    [SerializeField]
    private Vector2 spawnLimits;

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
        // Determines Which SpawnPoints Are On The Screen And Which Aren't
        availableSpawnPoints = new List<SpawnPoint>();
        foreach (SpawnPoint sp in allSpawnPoints)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(sp.transform.position);
            if ((screenPos.x > (Screen.width * spawnLimits.x)) && (screenPos.x < (Screen.width - (Screen.width * spawnLimits.x))) && (screenPos.y > (Screen.height * spawnLimits.y)) && (screenPos.y < (Screen.height - (Screen.height * spawnLimits.y))))
            {
                availableSpawnPoints.Add(sp);
                sp.IsViable = true;
            }
            else
            {
                sp.IsViable = false;
            }
        }
    }

    public void SpawnAudienceMember()
    {
        GameObject newAudienceMember = Instantiate(audienceMemberPrefab, this.transform);

        Vector3 distance = Vector3.zero;

        // Depth Position
        int pickedSpawn = Random.Range(0, availableSpawnPoints.Count);
        newAudienceMember.transform.position = availableSpawnPoints[pickedSpawn].transform.position;
        newAudienceMember.transform.rotation = Quaternion.identity;
    }

    private void OnDrawGizmos()
    {
        foreach (SpawnPoint sp in allSpawnPoints)
        {
            if (sp.IsViable)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }

            Gizmos.DrawWireSphere(sp.transform.position, 0.5f);
        }
    }
}
