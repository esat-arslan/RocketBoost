using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 50f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        transform.Rotate(Vector3.forward * rotationStrength * Time.fixedDeltaTime * rotationDirection * -1);
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        }
    }
}
