using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class Initialization : MonoBehaviour
{
    [NonSerialized] private Camera _camera;
    [NonSerialized] private CinemachineVirtualCamera _CM;
    [NonSerialized] private CamController _camController;
    [NonSerialized] private VerticalPlayerMovement _verticalPlayerMovement;


    private void Init()
    {
        _camera = FindObjectOfType<Camera>();
        _CM = FindObjectOfType<CinemachineVirtualCamera>();
        _camController = FindObjectOfType<CamController>();
        _verticalPlayerMovement = FindObjectOfType<VerticalPlayerMovement>();
    }
    private void InitOther()
    {
        _camController.Camera = _camera;
        _camController.CM = _CM;
        _verticalPlayerMovement.Camera = _camera;
    }

    private void Awake() { Init(); InitOther(); } 
    }
