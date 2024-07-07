using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    private Initialization _init;
    [SerializeField] private float _playerMovementPredictionCoef_horizontal;
    [SerializeField] private float _playerMovementPredictionCoef_vertical;
    [SerializeField] private Transform _defaultTransform;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxDistance_z;
    [SerializeField] private float _maxDistance_y;
    private Rigidbody _playerRB;
    private HorizontalPlayerMovement _horizontalPlayerMovement;
    private VerticalPlayerMovement _verticalPlayerMovement;
    private CamController _camController;
    private void Start()
    {
        Init();
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _defaultTransform.position + new Vector3(0, Mathf.Clamp(_verticalPlayerMovement.VerticalVelocity, -_maxDistance_y, _maxDistance_y),
            Mathf.Clamp(_playerRB.velocity.z * _playerMovementPredictionCoef_horizontal, -_maxDistance_z, _maxDistance_z)), _speed * Time.fixedDeltaTime);
    }
    private void Init()
    {
        _camController = FindObjectOfType<CamController>();
        _verticalPlayerMovement = FindObjectOfType<VerticalPlayerMovement>();
        _horizontalPlayerMovement = FindObjectOfType<HorizontalPlayerMovement>();
        _init = FindObjectOfType<Initialization>();
        _playerRB = _init.PlayerRigidbody;
    }
}
