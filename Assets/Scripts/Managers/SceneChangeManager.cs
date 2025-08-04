using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChangeManager : MonoBehaviour
{
    public void GoToGame()
    {
        SceneManager.LoadScene("InGame");
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("TitleMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
