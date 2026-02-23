using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    //closes the game
    public void QuitTheGame()
    {
        Application.Quit();
    }


    //loads level 1
    public void StartGameButton()
    {
        SceneManager.LoadScene("main game");      
    }

    //loads frontend
    public void BackToFrontendButton()
    {
        SceneManager.LoadScene("frontend");
    }
}
