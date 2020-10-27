using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCanvasScaler : MonoBehaviour
{
    [SerializeField]
    private Vector2 referenceResolution;

    private void Start()
    {

        Vector2 newScale = new Vector2
        {
            x = Screen.width / referenceResolution.x,
            y = Screen.height / referenceResolution.y
        };

        RectTransform rect = this.GetComponent<RectTransform>();
        Vector2 newSize = rect.sizeDelta;
        newSize.x *= newScale.x;
        newSize.y *= newScale.y;
        rect.sizeDelta = newSize;
    }
}
