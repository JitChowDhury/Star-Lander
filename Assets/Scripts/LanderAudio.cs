using System;
using UnityEngine;

public class LanderAudio : MonoBehaviour
{
    [SerializeField] private AudioSource thrusterAudioSource;
    private Lander lander;

    void Awake()
    {
        lander = GetComponent<Lander>();
    }
    void Start()
    {
        lander.OnNoForce += Lander_OnNoForce;
        lander.OnUpForce += Lander_OnUpForce;
        lander.OnLeftForce += Lander_OnLeftForce;
        lander.OnRightForce += Lander_OnRightForce;

        thrusterAudioSource.Pause();

    }

    private void Lander_OnRightForce(object sender, EventArgs e)
    {

        if (!thrusterAudioSource.isPlaying)
            thrusterAudioSource.Play();
    }

    private void Lander_OnLeftForce(object sender, EventArgs e)
    {
        if (!thrusterAudioSource.isPlaying)
            thrusterAudioSource.Play();
    }

    private void Lander_OnUpForce(object sender, EventArgs e)
    {
        if (!thrusterAudioSource.isPlaying)
            thrusterAudioSource.Play();
    }

    private void Lander_OnNoForce(object sender, EventArgs e)
    {
        thrusterAudioSource.Pause();
    }
}
