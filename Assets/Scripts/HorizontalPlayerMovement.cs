using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlayerMovement : MonoBehaviour
{
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;
    [SerializeField] private float _maxSpeed;
    private float _currentSpeed;
    private Vector3 _moveVector;
    private bool _isShouldMove;

    private void DirectionDefiningUpdate() { _isShouldMove = false;
                                             if (Input.GetKey(KeyCode.D)) { _moveVector = transform.right; _isShouldMove = true; }
                                             if (Input.GetKey(KeyCode.A)) { _moveVector = -transform.right; _isShouldMove = true; } }

    private void SpeedUpdate() { _currentSpeed = _isShouldMove ? Mathf.Clamp(_currentSpeed + _acceleration, 0, _maxSpeed) : Mathf.Clamp(_currentSpeed - _deceleration, 0, _maxSpeed); }

    private void MovementFixedUpdate() { transform.position += _moveVector * _currentSpeed * Time.fixedDeltaTime; }

    private void Update() { DirectionDefiningUpdate(); SpeedUpdate(); }

    private void FixedUpdate() { MovementFixedUpdate(); }
}

