using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamController : MonoBehaviour
{
    [SerializeField] private float _speed;
    public float _deadZoneWidth;
    public float _deadZoneHeight;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private Transform _defaultTransform;
    [SerializeField] private float _softZoneWidth;
    [SerializeField] private float _softZoneHeight;
    private Transform _playerTransform;
    private float _timeShake;
    private float _frequencyShake; // Сколько раз в секунду будет меняться положение камеры
    private float _amplitudeShake;
    public Camera Camera;
    
    private Initialization _init;
    

    private void Start()
    {
        Init();
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        CamMoveFixedUpdate();
    }

    public void StartShake(float time, float frequency, float amplitude)
    {
        _timeShake = time;
        _frequencyShake = frequency;
        _amplitudeShake = amplitude;
        StopCoroutine("ShakeCor");
        StartCoroutine(nameof(StopShake));
        StartCoroutine(nameof(Shake));
    }
    private IEnumerator StopShake()
    {
        yield return new WaitForSeconds(_timeShake);
        _timeShake = 0;
        _frequencyShake = 0;
        _amplitudeShake = 0;
    }
    private IEnumerator Shake()
    {
        while (_timeShake != 0)
        {
            float new_x = transform.position.x + Random.Range(-_amplitudeShake, _amplitudeShake);
            float new_y = transform.position.y + Random.Range(-_amplitudeShake, _amplitudeShake);
            float _new_z = transform.position.z + Random.Range(-_amplitudeShake, _amplitudeShake);
            transform.position = new Vector3(new_x, new_y, _new_z);
            yield return new WaitForSeconds(1 / _frequencyShake);
        }
    }

    private void CamMoveFixedUpdate()
    {
        if (_targetTransform.position.z < _defaultTransform.position.z + _deadZoneWidth && _targetTransform.position.z > _defaultTransform.position.z - _deadZoneWidth && Mathf.Abs(transform.position.z - _defaultTransform.position.z) > _deadZoneWidth)
        {
            if (transform.position.z < _defaultTransform.position.z)
            {
                float new_z = Mathf.Lerp(transform.position.z, _defaultTransform.position.z - _deadZoneWidth, _speed * Time.fixedDeltaTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, new_z);
            }
            else
            {
                float new_z = Mathf.Lerp(transform.position.z, _defaultTransform.position.z + _deadZoneWidth, _speed * Time.fixedDeltaTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, new_z);
            }
        }
        else if (Mathf.Abs(_targetTransform.position.z - _defaultTransform.position.z) > _deadZoneWidth/* || Mathf.Abs(transform.position.z - _defaultTransform.position.z) > _deadZoneWidth*/)
        {
            float new_z = Mathf.Lerp(transform.position.z, _targetTransform.position.z, _speed * Time.fixedDeltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, new_z);
        }

        if (_targetTransform.position.y < _defaultTransform.position.y + _deadZoneHeight && _targetTransform.position.y > _defaultTransform.position.y - _deadZoneHeight && Mathf.Abs(transform.position.y - _defaultTransform.position.y) > _deadZoneHeight)
        {
            if (transform.position.y < _defaultTransform.position.y)
            {
                float new_y = Mathf.Lerp(transform.position.y, _defaultTransform.position.y - _deadZoneHeight, _speed * Time.fixedDeltaTime);
                transform.position = new Vector3(transform.position.x, new_y, transform.position.z);
            }
            else
            {
                float new_y = Mathf.Lerp(transform.position.y, _defaultTransform.position.y + _deadZoneHeight, _speed * Time.fixedDeltaTime);
                transform.position = new Vector3(transform.position.x, new_y, transform.position.z);
            }
        }
        else if (Mathf.Abs(_targetTransform.position.y - _defaultTransform.position.y) > _deadZoneHeight || Mathf.Abs(transform.position.y - _defaultTransform.position.y) > _deadZoneHeight)
        {
            float new_y = Mathf.Lerp(transform.position.y, _targetTransform.position.y, _speed * Time.fixedDeltaTime);
            transform.position = new Vector3(transform.position.x, new_y, transform.position.z);
        }
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _defaultTransform.position.y - _softZoneHeight, _defaultTransform.position.y + _softZoneHeight),
            Mathf.Clamp(transform.position.z, _defaultTransform.position.z - _softZoneWidth, _defaultTransform.position.z + _softZoneWidth));
    }

    private void Init()
    {
        _init = FindObjectOfType<Initialization>();
        _playerTransform = FindObjectOfType<VerticalPlayerMovement>().transform;
    }
}
