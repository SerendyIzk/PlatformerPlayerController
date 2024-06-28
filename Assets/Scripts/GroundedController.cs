using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroundedController : MonoBehaviour
{
    [NonSerialized] public bool IsGrounded;
    [SerializeField] private float _xAxisOffset;
    [SerializeField] private float _zAxisOffset;
    [SerializeField] private float _height;
    public Action OnTochedGround;
    private BoxCollider _bc;
    private FallingObjectSizeScaling _parentVerticalSizeScaling;
    private float _baseVerticalbcScaling;

    private void DrawCollider() { Vector3 size = GetComponentInParent<MeshFilter>().mesh.bounds.size;
        _bc.size = new Vector3(size.x - _xAxisOffset, _height, size.z - _zAxisOffset);
        _bc.center = new Vector3(0, -(size.y / 2), 0); _baseVerticalbcScaling = _bc.size.y; }

    private void ColliderUpdate() { if (_parentVerticalSizeScaling == null) return;
        _bc.size = new Vector3(_bc.size.x, _baseVerticalbcScaling / (transform.parent.transform.localScale.y / _parentVerticalSizeScaling.BaseVerticalScale), _bc.size.z); }

    private void OnTriggerEnter(Collider col) { if (col.CompareTag("Ground")) { IsGrounded = true; OnTochedGround?.Invoke(); } }

    private void OnTriggerExit(Collider col) { if (col.CompareTag("Ground")) IsGrounded = false; }

    private void Start() { _bc = GetComponent<BoxCollider>(); DrawCollider(); _parentVerticalSizeScaling = transform.parent.GetComponent<FallingObjectSizeScaling>(); }

    private void Update() { ColliderUpdate(); }
}
