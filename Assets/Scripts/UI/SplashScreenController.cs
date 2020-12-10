using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitThenMoveToMainMenu());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WaitThenMoveToMainMenu()
    {
        yield return new WaitForSecondsRealtime(3.0f);

        SceneManager.LoadScene("Main_Menu");
    }
}
