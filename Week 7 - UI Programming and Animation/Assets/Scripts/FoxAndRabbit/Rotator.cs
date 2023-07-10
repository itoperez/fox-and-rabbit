using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Header("Rotaton Contrls")]
    [Tooltip("The axis around which the gameObject is rotating")]
    [SerializeField]
    private Vector3 rotationAxis = Vector3.up;
    [Tooltip("The speed with which the gameObject is rotating measured in degrees/frame")]
    [SerializeField]
    private float rotationSpeed = 1f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}
