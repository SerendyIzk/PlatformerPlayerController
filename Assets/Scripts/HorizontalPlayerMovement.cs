using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlayerMovement : MonoBehaviour
{
    public Vector3 MoveVector { get; private set; }
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxSpeed;
    private Rigidbody _rb;
    private Initialization _init;

    private void MoveVectorDefiningUpdate() { MoveVector = Vector3.zero;
                                              if (Input.GetKey(KeyCode.D)) { MoveVector = transform.right; }
                                              if (Input.GetKey(KeyCode.A)) { MoveVector = -transform.right; } }

    private void MovementFixedUpdate() { _rb.AddForce(MoveVector * _acceleration, ForceMode.Acceleration); }

    private void MovementSpeedLimitUpdate() { _rb.velocity = new Vector3(Mathf.Clamp(_rb.velocity.x, -_maxSpeed, _maxSpeed), _rb.velocity.y, _rb.velocity.z); }

    private void Start() { _init = FindObjectOfType<Initialization>(); _rb = _init.PlayerRigidbody; }

    private void Update() { MoveVectorDefiningUpdate(); MovementSpeedLimitUpdate(); }

    private void FixedUpdate() { MovementFixedUpdate(); }
}

