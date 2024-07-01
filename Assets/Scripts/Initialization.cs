using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class Initialization : MonoBehaviour
{
    [NonSerialized] public Camera _camera;
    [NonSerialized] public CamController _camController;
    [NonSerialized] public VerticalPlayerMovement _verticalPlayerMovement;
    [NonSerialized] public Rigidbody PlayerRigidbody;

    private void Awake()
    {
        Init();
        InitOther();
    }
    private void Init() 
    {
        _camera = FindObjectOfType<Camera>();
        _camController = FindObjectOfType<CamController>();
        _verticalPlayerMovement = FindObjectOfType<VerticalPlayerMovement>();
        PlayerRigidbody = _verticalPlayerMovement.GetComponent<Rigidbody>(); 
    }

    private void InitOther()
    {
        _camController.Camera = _camera;
        _verticalPlayerMovement.Camera = _camera;
    }

}
