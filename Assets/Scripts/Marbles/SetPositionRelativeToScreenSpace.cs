using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionRelativeToScreenSpace : MonoBehaviour
{
    [SerializeField]
    private float depth;
    [SerializeField]
    private RectTransform targetPos;
    [SerializeField, EnumFlags]
    private AxisLocks locks;
    [SerializeField]
    private Vector3 lockPositions;

    // Start is called before the first frame update
    void Start()
    {
        // Calculating world pos based on screen pos with pre set depth test
        Vector2 screenPos = targetPos.transform.position;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, depth));

        // Locking Different Axis
        // X Axis
        if ((locks & AxisLocks.LIMIT_X) == AxisLocks.LIMIT_X)
        {
            worldPos.x = lockPositions.x;
        }
        // Y Axis
        if ((locks & AxisLocks.LIMIT_Y) == AxisLocks.LIMIT_Y)
        {
            worldPos.y = lockPositions.y;
        }
        // Z Axis
        if ((locks & AxisLocks.LIMIT_Z) == AxisLocks.LIMIT_Z)
        {
            worldPos.z = lockPositions.z;
        }

        this.transform.position = worldPos;
    }

    [System.Flags]
    private enum AxisLocks
    {
        Nothing = 0,
        LIMIT_X = 2,
        LIMIT_Y = 4,
        LIMIT_Z = 8,
        Everything = 0xFFFFFF
    }
}