using UnityEngine.SceneManagement;
using UnityEngine;


public class Menu : MonoBehaviour
{
    
    public void playFunct()
    {
        SceneManager.LoadScene(1);
    }

    public void backToMenu(){
        SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
