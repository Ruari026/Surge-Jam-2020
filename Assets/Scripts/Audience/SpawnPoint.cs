using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool IsViable;

    void OnEnable()
    {
        CheckIfViable();
    }

    private void CheckIfViable()
    {
        // Gets the Point's Position In Screen Space
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);

        // If it is off the screen then the point is not viable
        if (screenPos.x <= 0.0f || screenPos.x >= 1.0f)
        {
            IsViable = false;
        }
        else if (screenPos.y <= 0.0f || screenPos.y >= 1.0f)
        {
            IsViable = false;
        }
        else
        {
            IsViable = true;
        }
    }
}
