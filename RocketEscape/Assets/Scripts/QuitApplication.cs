using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        quitApplication();
    }

    void quitApplication()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Player hit Escape key");
            Application.Quit();
        }
    }
}
