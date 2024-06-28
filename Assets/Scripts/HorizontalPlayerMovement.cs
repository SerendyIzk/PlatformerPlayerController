using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlayerMovement : MonoBehaviour
{
    public Vector3 MoveVector { get; private set; }
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;
    [SerializeField] private float _maxSpeed;
    private float _currentSpeed;
    private bool _isShouldMove;

    private void DirectionDefiningUpdate() { _isShouldMove = false;
                                             if (Input.GetKey(KeyCode.D)) { MoveVector = transform.right; _isShouldMove = true; }
                                             if (Input.GetKey(KeyCode.A)) { MoveVector = -transform.right; _isShouldMove = true; } }

    private void SpeedUpdate() { _currentSpeed = _isShouldMove ? Mathf.Clamp(_currentSpeed + _acceleration, 0, _maxSpeed) : Mathf.Clamp(_currentSpeed - _deceleration, 0, _maxSpeed); }

    private void MovementFixedUpdate() { transform.position += MoveVector * _currentSpeed * Time.fixedDeltaTime; }

    private void Update() { DirectionDefiningUpdate(); SpeedUpdate(); }

    private void FixedUpdate() { MovementFixedUpdate(); }
}

