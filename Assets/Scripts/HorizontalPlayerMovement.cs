using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlayerMovement : DashingObject
{
    public Vector3 MoveVector { get; private set; }
    [SerializeField] [Range(0f, 1f)] private float _slowMotionScale;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxSpeed;
    private float _dashForce;
    private float _fixedDeltaTime;
    [SerializeField] private BarFillerUpdater _dashForceBar;

    private void DashPreparingUpdate() { if (Input.GetKeyDown(KeyCode.E) && !GC.IsGrounded && _dashAvailable) {
            StartCoroutine(nameof(DashCharging)); _dashAvailable = false; _dashForce = MinDashForce; Time.timeScale = _slowMotionScale; SaveFixedUpdateRate(); } }

    private IEnumerator DashCharging() { StartCoroutine(nameof(DashWaitUntilActivate)); 
        while (true) { _dashForce = Mathf.Clamp(_dashForce += DashChargeSpeed, MinDashForce, MaxDashForce); yield return null; } }

    private IEnumerator DashWaitUntilActivate() { StartCoroutine(nameof(DashConditionCheck));
        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.E)); Dash(); ReturnBasePassageOfTime(); StopNeededCnsOnDash(); }

    private IEnumerator DashConditionCheck() { yield return new WaitUntil(() => GC.IsGrounded); ReturnBasePassageOfTime(); StopAllCoroutines(); } // !WARNING! Stopping All coroutines

    protected override void Dash() { VelocityOnStart = _rb.velocity.z; StartCoroutine(nameof(NullifyingVerticalVelocityWhileDashingUpdate));
        _rb.AddForce(MoveVector * _dashForce, ForceMode.Impulse); }

    private void SaveFixedUpdateRate() { Time.fixedDeltaTime = _fixedDeltaTime * Time.timeScale; }

    private void ReturnBasePassageOfTime() { Time.timeScale = 1f; SaveFixedUpdateRate(); _dashForce = 0; }

    private void DashForceBarUpdate() { _dashForceBar.FillerUpdate(MinDashForce, MaxDashForce, _dashForce, true); }

    private void StopNeededCnsOnDash() {
        StopCoroutine(nameof(DashCharging));
        StopCoroutine(nameof(DashWaitUntilActivate));
        StopCoroutine(nameof(DashConditionCheck)); }

    private void MoveVectorDefiningUpdate() { MoveVector = Vector3.zero;
                                              if (Input.GetKey(KeyCode.D)) { MoveVector = transform.right; }
                                              if (Input.GetKey(KeyCode.A)) { MoveVector = -transform.right; } }

    private void MovementFixedUpdate() { _rb.AddForce(MoveVector * _acceleration, ForceMode.Acceleration); }

    private void MovementSpeedLimitUpdate() { _rb.velocity = new Vector3(Mathf.Clamp(_rb.velocity.x, -_maxSpeed, _maxSpeed), _rb.velocity.y, _rb.velocity.z); }

    new private void Awake() { base.Awake(); _fixedDeltaTime = Time.fixedDeltaTime; }

    private void Update() { MoveVectorDefiningUpdate(); MovementSpeedLimitUpdate(); DashPreparingUpdate(); DashForceBarUpdate(); }

    private void FixedUpdate() { MovementFixedUpdate(); }
}

