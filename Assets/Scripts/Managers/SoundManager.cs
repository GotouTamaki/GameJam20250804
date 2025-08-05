using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // グローバルアクセス用のシングルトンインスタンス
    public static SoundManager Instance { get; private set; }

    [Header("オーディオソース")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("BGM")]
    public AudioClip titleBgm;
    public AudioClip gameBgm;

    [Header("効果音")]
    public AudioClip startButtonSfx;
    public AudioClip buttonSfx;
    public AudioClip shootSfx;
    public AudioClip comboSfx;
    public AudioClip countdownSfx;
    public AudioClip countdownOverSfx;
    public AudioClip receiptPrintSfx;
    public bool startButtonSfxHasPlayed = false;

    [Header("音量設定")]
    [SerializeField, Range(0f, 1f)] private float sfxVolume = 0.3f;
    [SerializeField, Range(0f, 1f)] private float musicVolume = 0.1f;

    private void Awake()
    {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

        musicVolume = PlayerPrefs.GetFloat("MusicVolume", musicVolume);
        sfxVolume = 0.5f;

        ApplyVolumes();
    }

    private void Start()
    {
        foreach (var btn in GameObject.FindGameObjectsWithTag("SoundButton"))
            btn.GetComponent<Button>().onClick.AddListener(PlayButtonSFX);

        // 現在のシーンに応じたBGMを再生
        Scene currentScene = SceneManager.GetActiveScene();
        PlayMusicForScene(currentScene.name);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);

        foreach (var btn in GameObject.FindGameObjectsWithTag("SoundButton"))
            btn.GetComponent<Button>().onClick.AddListener(PlayButtonSFX);
    }

    // シーン読み込み時に適切なBGMを再生
    private void PlayMusicForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "TitleMenu":
                PlayMusic(titleBgm);
                break;
            case "InGame":
                musicSource.Stop();
                break;
            default:
                break;
        }
    }

    // 指定された音量で効果音を1回だけ再生
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    // BGMを再生または切り替え（同じ曲なら再再生しない）
    public void PlayMusic(AudioClip musicClip, bool loop = true)
    {
        if (musicSource.clip == musicClip && musicSource.isPlaying) return;

        musicSource.clip = musicClip;
        musicSource.loop = loop;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    // 特定の効果音を再生する便利メソッド
    public void PlayStartSFX()
    {
        if (startButtonSfxHasPlayed) return;

        PlaySFX(startButtonSfx);
        startButtonSfxHasPlayed = true;
    }

    public void PlayButtonSFX() => PlaySFX(buttonSfx);
    public void PlayShootSFX() => PlaySFX(shootSfx);
    public void PlayComboSFX() => PlaySFX(comboSfx);
    public void PlayCountdownSFX() => PlaySFX(countdownSfx);
    public void PlayCountdownOverSFX() => PlaySFX(countdownOverSfx);
    public void PlayReceiptSFX() => PlaySFX(receiptPrintSfx);


    // BGM音量を設定して保存
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = musicVolume;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.Save();
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    // 音量設定をオーディオソースに適用
    private void ApplyVolumes()
    {
        musicSource.volume = musicVolume;
    }
}
