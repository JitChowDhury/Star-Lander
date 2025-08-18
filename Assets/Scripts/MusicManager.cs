using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    private static float musicTime;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.time = musicTime;
    }

    void Update()
    {

        musicTime = audioSource.time;
    }


}
