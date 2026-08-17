using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenUI : MonoBehaviour
{
    public void ClickPlay(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ClickQuit()
    {
        Application.Quit();
    }
}
