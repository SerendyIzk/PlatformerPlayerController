using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarFillerUpdater : MonoBehaviour
{
    private RectTransform _rectTransform;
    private RawImage _ri;
    private RawImage _bg;

    public void FillerUpdate(float _minValue, float _maxValue, float _currentValue, bool _hideOnEmpty) { _rectTransform.anchorMax =
            new Vector2(Mathf.Lerp(0, 1, (_currentValue - _minValue)  / (_maxValue - _minValue)), _rectTransform.anchorMax.y);
        if (_hideOnEmpty) {
            if (_currentValue <= _minValue) { _ri.enabled = false; _bg.enabled = false; }
            else { _ri.enabled = true; _bg.enabled = true; } } }

    private void Start() { _rectTransform = GetComponent<RectTransform>(); _ri = GetComponent<RawImage>(); _bg = transform.parent.GetComponent<RawImage>(); }    
}
