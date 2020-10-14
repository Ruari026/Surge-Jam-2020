using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebsiteLink : MonoBehaviour
{
    public void MarbleLink()
    {
        Application.OpenURL("http://www.unbosi.org/marbles/");
    }

    public void CollabLink()
    {
        Application.OpenURL("https://appdeveloperscotland.co.uk/");
    }
}
