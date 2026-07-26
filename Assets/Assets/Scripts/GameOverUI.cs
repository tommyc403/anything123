using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{

    public void ClickPlayAgain()

    {
        SceneManager.LoadScene("Asteroids");
    }

    public void ClickMainMenu()
    {
        SceneManager.LoadScene("Title");
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
