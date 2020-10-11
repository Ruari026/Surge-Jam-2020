using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenController : MonoBehaviour
{
    private static LoadingScreenController _Instance;
    public static LoadingScreenController instance
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

    private void OnEnable()
    {
        if (_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.LogError("ERROR: Instance Already Exists");
            Destroy(this.gameObject);
        }
    }

    [SerializeField]
    private Animator theAnimController;

    public void TriggerLoadingScreen(Action myMethodName)
    {
        StartCoroutine(RunLoadingScreen(myMethodName));
    }

    private IEnumerator RunLoadingScreen(Action myMethodName)
    {
        theAnimController.SetTrigger("Trigger");

        yield return new WaitForSeconds(1.0f);
        myMethodName.Invoke();
    }
}
