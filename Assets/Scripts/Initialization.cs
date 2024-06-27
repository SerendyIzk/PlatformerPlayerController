using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class Initialization : MonoBehaviour
{ /*
    [NonSerialized] public GameObject Player;
    [NonSerialized] public Rigidbody PlayerRb;
    [NonSerialized] public GroundedController PlayerGroundedController;
    */
    [NonSerialized] private Camera _camera;
    [NonSerialized] private CinemachineVirtualCamera _CM;
    [NonSerialized] private CamController _camController;
    

    private void Init()
    {
        /*Player = FindObjectOfType<VerticalPlayerMovement>().gameObject;
        PlayerRb = Player.GetComponent<Rigidbody>();
        PlayerGroundedController = Player.transform.GetChild(0).GetComponent<GroundedController>();*/
        _camera = FindObjectOfType<Camera>();
        _CM = FindObjectOfType<CinemachineVirtualCamera>();
        _camController = FindObjectOfType<CamController>();
    }
    private void InitOther()
    {
        _camController.Camera = _camera;
        _camController.CM = _CM;
    }

    private void Awake() { Init(); InitOther(); } 
    }
