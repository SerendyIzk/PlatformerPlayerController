using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectSizeScaling : MonoBehaviour
{
    [SerializeField] private float _maxVerticalScale;
    [SerializeField] private float _minVerticalScale;
    public float BaseVerticalScale { get; private set; }
    private FallingObject _fo;
    private MeshFilter _mf;

    private void VerticalScaleUpdate() { Vector3 localscale = transform.localScale; Vector3 pos = transform.position;
        if (_fo.GC.IsGrounded) {
            var offset = (transform.localScale.y - BaseVerticalScale) * _mf.mesh.bounds.size.y;
            transform.localScale = new Vector3(localscale.x, BaseVerticalScale, localscale.z);
            transform.position = new Vector3(pos.x, pos.y - offset, pos.z); }
        else transform.localScale = new Vector3(localscale.x, Mathf.Lerp(_minVerticalScale, _maxVerticalScale, Mathf.Abs(_fo.VerticalVelocity / _fo.MaxVerticalVelocity)), localscale.z); }

    private void Start() { _fo = GetComponent<FallingObject>(); BaseVerticalScale = transform.localScale.y; _mf = GetComponent<MeshFilter>(); }

    private void Update() { VerticalScaleUpdate(); }
}
