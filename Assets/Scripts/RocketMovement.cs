using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    AudioSource audioSource;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 50f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBoosterParticle;
    [SerializeField] ParticleSystem leftBoosterParticle;
    [SerializeField] ParticleSystem rightBoosterParticle;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float rotatationInput = rotation.ReadValue<float>();
        if (rotatationInput < 0)
        {
            ApplyRotation(rotatationInput);
        }
        else if (rotatationInput > 0)
        {
            ApplyRotation(rotatationInput);
        }
    }

    private void ApplyRotation(float rotationDirection)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationStrength * Time.fixedDeltaTime * rotationDirection * -1);
        rb.freezeRotation = false;

        HandleBoosterParticles(rotationDirection);
    }

    private void HandleBoosterParticles(float rotationDirection)
    {
        if (rotationDirection > 0)
        {
            if (!rightBoosterParticle.isPlaying) rightBoosterParticle.Play();
            leftBoosterParticle.Stop();
        }
        else if (rotationDirection < 0 )
        {
            if (!leftBoosterParticle.isPlaying) leftBoosterParticle.Play();
            rightBoosterParticle.Stop();
        }
        else
        {
            leftBoosterParticle.Stop();
            rightBoosterParticle.Stop();
        }
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainBoosterParticle.Play();
    }
}
