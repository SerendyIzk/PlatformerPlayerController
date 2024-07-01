using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public float Gravity;
    [SerializeField] private float _maxVerticalVelocity;
    public float MaxVerticalVelocity { get { return _maxVerticalVelocity; } }
    public float VerticalVelocity { get; protected set; }
    public GroundedController GC { get; protected set; }

    private void FallingFixedUpdate() { VerticalVelocity = GC.IsGrounded ? 0 : Mathf.Clamp(VerticalVelocity - Gravity, -_maxVerticalVelocity, _maxVerticalVelocity);
        transform.position += Vector3.up * VerticalVelocity * Time.fixedDeltaTime; }

    private void Awake() { GC = transform.GetChild(0).GetComponent<GroundedController>(); }

    private void FixedUpdate() { FallingFixedUpdate(); }
}
