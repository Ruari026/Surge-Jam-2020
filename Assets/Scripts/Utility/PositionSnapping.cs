using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteAlways]
public class PositionSnapping : MonoBehaviour
{
    [SerializeField]
    private float xLock = 1.0f;
    [SerializeField]
    private float yLock = 1.0f;
    [SerializeField]
    private float zLock = 1.0f;

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR
        if (Selection.Contains(this.gameObject))
        {
            // Snapping Position
            Vector3 snapPos = this.transform.localPosition;

            snapPos.x = (Mathf.Round((snapPos.x / xLock))) * xLock;
            snapPos.y = (Mathf.Round((snapPos.y / yLock))) * yLock;
            snapPos.z = (Mathf.Round((snapPos.z / zLock))) * zLock;

            this.transform.localPosition = snapPos;
        }
#endif
    }
}
