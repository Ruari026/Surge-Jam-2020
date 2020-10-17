using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCameraGetter : MonoBehaviour
{
    private Canvas theCanvas;

    // Start is called before the first frame update
    void Start()
    {
        theCanvas = this.GetComponent<Canvas>();

        Camera mainCamera = Camera.main;

        theCanvas.worldCamera = mainCamera;
    }
}
