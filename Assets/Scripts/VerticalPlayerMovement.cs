using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlayerMovement : JumpingObject
{
    [SerializeField] private float _coyoteTimeInSeconds;
    [SerializeField] private float _bufferJumpTimeInSeconds;
    [SerializeField] private float _jumpCooldown;
    private bool _isJumpOnCooldown;
    private bool _isJumped = true;
    private bool _isAirJumped = true;

    protected override void Jump() { if (_isJumpOnCooldown) return;
        GC.IsGrounded = false; VerticalVelocity = JumpForce;
        _isJumpOnCooldown = true; Invoke(nameof(ReloadJump), _jumpCooldown); _isJumped = true; }

    private void JumpingUpdate() { if (GC.IsGrounded) { _isJumped = false; _isAirJumped = false; }
                                   if (Input.GetKeyDown(KeyCode.Space)) if (GC.IsGrounded) Jump(); else StartCoroutine(nameof(WaitUntilGrounded));
                                   if (!GC.IsGrounded && !_isJumped) StartCoroutine(nameof(WaitUntilJump)); }

    private void AirJumpingUpdate() { if (Input.GetKeyDown(KeyCode.Space) && !_isAirJumped && !GC.IsGrounded && !IsInvoking(nameof(CoyoteTime)) && !IsInvoking(nameof(BufferJumpTime))) { _isAirJumped = true; Jump(); } }

    private void ReloadJump() { _isJumpOnCooldown = false; }

    private IEnumerator WaitUntilGrounded() { Invoke(nameof(BufferJumpTime), _bufferJumpTimeInSeconds);
        yield return new WaitUntil(() => GC.IsGrounded); Jump(); }

    private void BufferJumpTime() { StopCoroutine(nameof(WaitUntilGrounded)); }

    private IEnumerator WaitUntilJump() { Invoke(nameof(CoyoteTime), _coyoteTimeInSeconds);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space)); Jump(); }

    private void CoyoteTime() { StopCoroutine(nameof(WaitUntilJump)); }

    private void Update() { AirJumpingUpdate(); JumpingUpdate(); }
}
