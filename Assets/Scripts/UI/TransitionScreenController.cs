using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScreenController : MonoBehaviour
{
    private static TransitionScreenController _Instance;
    public static TransitionScreenController instance
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

    [SerializeField]
    private Animator theAnimController;
    [SerializeField]
    private bool startFadedIn;
    private bool isFadedIn;

    private void OnEnable()
    {
        if (_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(this);

            theAnimController.SetBool("StartIn", startFadedIn);
            isFadedIn = startFadedIn;
        }
        else
        {
            Debug.Log("Instance Already Exists, Destroying This...");
            Destroy(this.gameObject);
        }
    }

    public void FadeIn()
    {
        if (!isFadedIn)
        {
            theAnimController.SetTrigger("FadeIn");
            isFadedIn = true;
        }
    }

    public void FadeOut()
    {
        if (isFadedIn)
        {
            theAnimController.SetTrigger("FadeOut");
            isFadedIn = false;
        }
    }
}
