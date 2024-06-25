using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlayerMovement : JumpingObject
{
    [SerializeField] private float _bufferJumpTimeInSeconds;
    [SerializeField] private float _jumpCooldown;
    private bool _isJumpOnCooldown;

    protected override void Jump() { if (_isJumpOnCooldown) return;
        GC.IsGrounded = false; VerticalVelocity = JumpForce;
        _isJumpOnCooldown = true; Invoke(nameof(ReloadJump), _jumpCooldown); }

    private void JumpingUpdate() { if (Input.GetKeyDown(KeyCode.Space)) if (GC.IsGrounded) Jump(); else StartCoroutine(nameof(WaitUntilGrounded)); }

    private void ReloadJump() { _isJumpOnCooldown = false; }

    private IEnumerator WaitUntilGrounded() { StartCoroutine(nameof(BufferJumpTime));
        yield return new WaitUntil(() => GC.IsGrounded); Jump(); }

    private IEnumerator BufferJumpTime() { yield return new WaitForSeconds(_bufferJumpTimeInSeconds); StopCoroutine(nameof(WaitUntilGrounded)); }

    private void Update() { JumpingUpdate(); }
}
