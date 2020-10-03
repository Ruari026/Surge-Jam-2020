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
    private Vector3 maxPos;
    [SerializeField]
    private float maxWidth = 5;
    [SerializeField]
    private Vector3 minPos;
    [SerializeField]
    private float minWidth = 1;

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

    private void DetermineSpawnBounds()
    {

    }

    public void SpawnAudienceMember()
    {
        GameObject newAudienceMember = Instantiate(audienceMemberPrefab, this.transform);

        Vector3 distance = Vector3.zero;
        
        // Depth Position
        distance.y = Random.Range(minPos.y, maxPos.y);

        // Horizontal Position
        float amount = (distance.y - minPos.y) / (maxPos.y - minPos.y);
        float range = Mathf.Lerp(minWidth, maxWidth, amount);
        distance.x = Random.Range(-range, range);

        newAudienceMember.transform.localPosition = distance;
        newAudienceMember.transform.rotation = Quaternion.identity;
    }
}
