using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class SceneChangeManager : MonoBehaviour
{
    public GameObject soundManagerPrefab;
    private bool isLoading = false;

    void Awake()
    {
        if (SoundManager.Instance == null)
        {
            Instantiate(soundManagerPrefab);
        }
    }

    public void GoToGame()
    {
        if (!isLoading)
        {
            isLoading = true;
            StartCoroutine(WaitUntilSFXHasFinished());
        }
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("TitleMenu");
        SoundManager.Instance.startButtonSfxHasPlayed = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitUntilSFXHasFinished()
    {
        if (!SoundManager.Instance.startButtonSfxHasPlayed)
        {
            SoundManager.Instance.musicSource.Stop();
            SoundManager.Instance.PlayStartSFX();
            SoundManager.Instance.startButtonSfxHasPlayed = true;
        }

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("InGame");
    }

}
