using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedController : MonoBehaviour
{
    public bool IsGrounded { 
        private set;
        get; }
    [SerializeField] private float _xAxisOffset;
    [SerializeField] private float _zAxisOffset;
    [SerializeField] private float _height;
    private BoxCollider _bc;

    private void DrawCollider() { Vector3 size = GetComponentInParent<MeshFilter>().mesh.bounds.size;
        _bc.size = new Vector3(size.x - _xAxisOffset, _height, size.z - _zAxisOffset);
        _bc.center = new Vector3(0, -(size.y / 2), 0); }

    private void OnTriggerEnter(Collider col) { if (col.CompareTag("Ground")) IsGrounded = true; }

    private void OnTriggerExit(Collider col) { if (col.CompareTag("Ground")) IsGrounded = false; }

    private void Start() { _bc = GetComponent<BoxCollider>(); DrawCollider(); }
}
