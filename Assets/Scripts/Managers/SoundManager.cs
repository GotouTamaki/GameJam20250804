using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    // グローバルアクセス用のシングルトンインスタンス
    public static SoundManager Instance { get; private set; }

    [Header("オーディオソース")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    [Header("BGM")]
    [SerializeField] private AudioClip titleBgm;
    [SerializeField] private AudioClip gameBgm;

    [Header("効果音")]
    [SerializeField] private AudioClip startButtonSfx;
    [SerializeField] private AudioClip buttonSfx;
    [SerializeField] private AudioClip shootSfx;
    [SerializeField] private AudioClip comboSfx;

    [Header("音量設定")]
    [SerializeField, Range(0f, 1f)] private float sfxVolume = 0.3f;
    [SerializeField, Range(0f, 1f)] private float musicVolume = 0.1f;

    private void Awake()
    {
        // シングルトン処理
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 保存された音量設定を読み込む
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", musicVolume);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", sfxVolume);

        ApplyVolumes();
    }

    private void Start()
    {
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
                PlayMusic(gameBgm);
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
    public void PlayStartSFX() => PlaySFX(startButtonSfx);
    public void PlayButtonSFX() => PlaySFX(buttonSfx);
    public void PlayShootSFX() => PlaySFX(shootSfx);
    public void PlayComboSFX() => PlaySFX(comboSfx);

    // BGM音量を設定して保存
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = musicVolume;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.Save();
    }

    // 効果音音量を設定して保存
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        sfxSource.volume = sfxVolume;
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    // 音量設定をオーディオソースに適用
    private void ApplyVolumes()
    {
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }
}
