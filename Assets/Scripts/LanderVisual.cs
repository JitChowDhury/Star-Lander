using System;
using UnityEngine;

public class LanderVisual : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftThrusterParticleSystem;
    [SerializeField] private ParticleSystem middleThrusterParticleSystem;
    [SerializeField] private ParticleSystem rightThrusterParticleSystem;


    private Lander lander;

    void Awake()
    {
        lander = GetComponent<Lander>();
        lander.OnUpForce += Lander_OnUpForce;
        lander.OnLeftForce += Lander_OnLeftForce;
        lander.OnRightForce += Lander_OnRightForce;
        lander.OnNoForce += Lander_OnNoForce;


        SetEnableThrusterParticles(leftThrusterParticleSystem, false);
        SetEnableThrusterParticles(middleThrusterParticleSystem, false);
        SetEnableThrusterParticles(rightThrusterParticleSystem, false);
    }

    private void Lander_OnNoForce(object sender, EventArgs e)
    {
        SetEnableThrusterParticles(leftThrusterParticleSystem, false);
        SetEnableThrusterParticles(middleThrusterParticleSystem, false);
        SetEnableThrusterParticles(rightThrusterParticleSystem, false);
    }

    private void Lander_OnRightForce(object sender, EventArgs e)
    {
        SetEnableThrusterParticles(leftThrusterParticleSystem, true);

    }

    private void Lander_OnLeftForce(object sender, EventArgs e)
    {

        SetEnableThrusterParticles(rightThrusterParticleSystem, true);
    }

    private void Lander_OnUpForce(object sender, EventArgs e)
    {
        SetEnableThrusterParticles(leftThrusterParticleSystem, true);
        SetEnableThrusterParticles(middleThrusterParticleSystem, true);
        SetEnableThrusterParticles(rightThrusterParticleSystem, true);
    }



    private void SetEnableThrusterParticles(ParticleSystem particleSystem, bool enabled)
    {
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        emissionModule.enabled = enabled;
    }
}
