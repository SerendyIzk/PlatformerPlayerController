using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlayerMovement : MonoBehaviour
{
    public float Gravity;
    public float JumpForce;
    [SerializeField] private float _coyoteTimeInSeconds;
    [SerializeField] private float _jumpCooldown;
    private Initialization _init;
    private Rigidbody _rb;
    private GroundedController _gc;
    private float _fallVelocity;
    private bool _isGrounded;
    private bool IsJumpOnCooldown;

    private void FallingFixedUpdate() { _isGrounded = _gc.IsGrounded; _fallVelocity = _isGrounded ? 0 : _fallVelocity - Gravity;
        transform.position += Vector3.up * _fallVelocity * Time.fixedDeltaTime; }

    private void JumpingUpdate() { if (Input.GetKeyDown(KeyCode.Space)) if (_isGrounded) Jump(); else StartCoroutine(nameof(WaitUntilGrounded)); }

    private void Jump() { if (IsJumpOnCooldown) return;
        _fallVelocity = 0; _rb.velocity = Vector3.zero; _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        IsJumpOnCooldown = true; Invoke(nameof(ReloadJump), _jumpCooldown); }

    private void ReloadJump() { IsJumpOnCooldown = false; }

    private IEnumerator WaitUntilGrounded() { StartCoroutine(nameof(CoyoteTime));
        yield return new WaitUntil(() => _isGrounded); Jump(); }

    private IEnumerator CoyoteTime() { yield return new WaitForSeconds(_coyoteTimeInSeconds); StopCoroutine(nameof(WaitUntilGrounded)); }

    private void Start() { _init = FindObjectOfType<Initialization>();
                           _rb = _init.PlayerRb;
                           _gc = _init.PlayerGroundedController; }

    private void Update() { JumpingUpdate(); }
   
    private void FixedUpdate() { FallingFixedUpdate(); }
}
