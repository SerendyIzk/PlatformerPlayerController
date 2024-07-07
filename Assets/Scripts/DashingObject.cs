using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingObject : MonoBehaviour
{
    public float VelocityOnStart { get; protected set; }
    public float DashChargeSpeed;
    public float MaxDashForce;
    public float MinDashForce;
    public GroundedController GC { get; private set; }
    protected bool _dashAvailable;
    protected Rigidbody _rb;
    private Initialization _init;

    protected virtual void Dash() { if (!GC.IsGrounded && _dashAvailable) { VelocityOnStart = _rb.velocity.z; StartCoroutine(nameof(NullifyingVerticalVelocityWhileDashingUpdate));
            _rb.AddForce(transform.forward * UnityEngine.Random.Range(MinDashForce, MaxDashForce), ForceMode.Impulse); _dashAvailable = false; } }

    protected IEnumerator NullifyingVerticalVelocityWhileDashingUpdate() { Vector3 vel = _rb.velocity; 
        while (Mathf.Abs(vel.z) > Mathf.Abs(VelocityOnStart)) _rb.velocity = new Vector3(vel.x, 0, vel.z); yield return null; }

    protected void ReloadDash() { _dashAvailable = true; }

    protected void Awake() { GC = transform.GetChild(0).GetComponent<GroundedController>(); }

    private void Start() { _init = FindObjectOfType<Initialization>(); _rb = _init.PlayerRigidbody; }

    private void OnEnable() { GC.OnTouchedGround += ReloadDash; }
    private void OnDisable() { GC.OnTouchedGround -= ReloadDash; }
}
