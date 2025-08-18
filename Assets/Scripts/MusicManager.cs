using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    public event EventHandler OnMusicChanged;
    private const int MUSIC_VOLUME_MAX = 10;
    private AudioSource audioSource;
    private static int musicVolume = 4;
    private static float musicTime;
    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.time = musicTime;
    }

    void Start()
    {
        audioSource.volume = GetMusicVolumeNormalized();
    }

    void Update()
    {

        musicTime = audioSource.time;
    }

    public void ChangeMusicVolume()
    {
        musicVolume = (musicVolume + 1) % MUSIC_VOLUME_MAX;
        audioSource.volume = GetMusicVolumeNormalized();
        OnMusicChanged?.Invoke(this, EventArgs.Empty);
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
