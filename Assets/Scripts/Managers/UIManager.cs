using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TMP_Text countdownText;  // Assign your countdown text here

    void Awake()
    {
        //Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void OnRulesPanelClosed()
    {
        StartCoroutine(CountdownAndStartMusic());
        SoundManager.Instance.PlayCountdownSFX();
    }

    private IEnumerator CountdownAndStartMusic()
    {
        if (countdownText == null)
            yield return null;

        string[] countdown = { "3", "2", "1", "Start" };

        SoundManager.Instance?.PlayCountdownSFX();

        for (int i = 0; i < countdown.Length; i++)
        {
            countdownText.text = countdown[i];

            // Play SFX when "START"
            if (i == countdown.Length - 1)
            {
                SoundManager.Instance.sfxSource.Stop();
                SoundManager.Instance?.PlayCountdownOverSFX();
            }

            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "";

        if (SoundManager.Instance != null) // Changes music on start
            SoundManager.Instance.PlayMusic(SoundManager.Instance.gameBgm);
    }
}
