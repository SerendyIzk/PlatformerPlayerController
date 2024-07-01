using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectSizeScaling : MonoBehaviour
{
    [SerializeField] private float _maxVerticalScale;
    [SerializeField] private float _minVerticalScale;
    [SerializeField] private float _maxHorizontalScale;
    [SerializeField] private float _minHorizontalScale;
    public float BaseVerticalScale { get; private set; }
    public float BaseHorizontalScale { get; private set; }
    private FallingObject _fo;
    private MeshFilter _mf;

    private void VerticalScaleUpdate() { Vector3 localscale = transform.localScale; Vector3 pos = transform.position;
        if (_fo.GC.IsGrounded) {
            var offset = Mathf.Abs((transform.localScale.y - BaseVerticalScale) * _mf.mesh.bounds.size.y);
            transform.localScale = new Vector3(localscale.x, BaseVerticalScale, localscale.z);
            transform.position = new Vector3(pos.x, pos.y - offset, pos.z); }
        else transform.localScale = new Vector3(localscale.x, Mathf.Lerp(_minVerticalScale, _maxVerticalScale, Mathf.Abs(_fo.VerticalVelocity / _fo.MaxVerticalVelocity)), localscale.z); }

    private void HorizontalScaleUpdate() { Vector3 localscale = transform.localScale;
        if (_fo.GC.IsGrounded) transform.localScale = new Vector3(BaseHorizontalScale, localscale.y, BaseHorizontalScale);
        else { float _horiscale = Mathf.Lerp(_maxHorizontalScale, _minHorizontalScale, Mathf.Abs(_fo.VerticalVelocity / _fo.MaxVerticalVelocity));
               transform.localScale = new Vector3( _horiscale, localscale.y, _horiscale); } }

    private void Start() { _fo = GetComponent<FallingObject>(); _mf = GetComponent<MeshFilter>();
                           BaseVerticalScale = transform.localScale.y; BaseHorizontalScale = transform.localScale.x; }

    private void Update() { VerticalScaleUpdate(); HorizontalScaleUpdate(); }
}
