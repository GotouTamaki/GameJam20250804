using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class SceneChangeManager : MonoBehaviour
{
    public GameObject soundManagerPrefab;

    void Awake()
    {
        if (SoundManager.Instance == null)
        {
            Instantiate(soundManagerPrefab);
        }
    }

    public void GoToGame()
    {
        StartCoroutine(WaitUntilSFXHasFinished());
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("TitleMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitUntilSFXHasFinished()
    {
        SoundManager.Instance.musicSource.Stop();
        SoundManager.Instance.PlayStartSFX();

        yield return new WaitForSeconds(2.8f);

        SceneManager.LoadScene("InGame");
    }

}
