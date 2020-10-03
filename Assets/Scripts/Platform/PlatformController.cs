using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField]
    private GameObject thePlatform;
    [SerializeField]
    private float maxAngle;
    
    [Range(0.05f, 1.0f)]
    public float instabilityAmount = 0;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = this.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        // Handling Instability
        Vector3 instabilityDirection = new Vector3();

        instabilityDirection.x = Mathf.PerlinNoise(Time.realtimeSinceStartup, 0);
        instabilityDirection.x = (instabilityDirection.x * 2) - 1;
        instabilityDirection.x = (instabilityDirection.x * instabilityAmount);

        instabilityDirection.z = Mathf.PerlinNoise(0, Time.realtimeSinceStartup);
        instabilityDirection.z = (instabilityDirection.z * 2) - 1;
        instabilityDirection.z = (instabilityDirection.z * instabilityAmount);

        if (instabilityDirection.magnitude > 1)
            instabilityDirection.Normalize();

        // Handling User Input
        Vector3 inputDirection = Vector3.zero;
#if UNITY_EDITOR
        // Editor Input
        inputDirection.z = -Input.GetAxis("Horizontal");
        inputDirection.x = Input.GetAxis("Vertical");
#endif
        if (inputDirection.magnitude > 1.0f)
            inputDirection.Normalize();

        TiltPlatform(instabilityDirection + inputDirection);
    }

    private void TiltPlatform(Vector3 direction)
    {
        if (direction.magnitude > 1)
            direction.Normalize();

        thePlatform.transform.rotation = Quaternion.Lerp(thePlatform.transform.rotation, Quaternion.Euler((direction * maxAngle) + offset), Time.deltaTime);
    }
}
