using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const int MUSIC_VOLUME_MAX = 10;

    public static MusicManager Instance { get; private set; }

    private static float musicTime;
    private static int musicVolume = 4;

    public event EventHandler OnMusicVolumeChanged;

    private AudioSource musicAudioSource;

    private void Awake()
    {
        if (Instance != null)
        {
            musicTime = musicAudioSource?.time ?? musicTime;
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.time = musicTime;
    }

    public void Start()
    {
        musicAudioSource.volume = GetMusicVolumeNormalized();
    }

    private void Update()
    {
        musicTime = musicAudioSource.time; 
    }

    public void ChangeMusicVolume()
    {
        musicVolume = (musicVolume + 1) % MUSIC_VOLUME_MAX;
        musicAudioSource.volume = GetMusicVolumeNormalized();
        OnMusicVolumeChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetMusicVolume()
    {
        return musicVolume;
    }

    public float GetMusicVolumeNormalized()
    {
        return ((float)musicVolume) / MUSIC_VOLUME_MAX;
    }
}
