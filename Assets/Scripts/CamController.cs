using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamController : MonoBehaviour
{
    public Camera Camera;
    public CinemachineVirtualCamera CM;
    private CinemachineBasicMultiChannelPerlin _noiseSettings;
    private CinemachineFramingTransposer CMFT;
    private HorizontalPlayerMovement _horizontalPlayerMovement;
    private Vector3 _moveVector;

    private void Start()
    {
        _horizontalPlayerMovement = FindObjectOfType<HorizontalPlayerMovement>();
        CMFT = CM.GetCinemachineComponent<CinemachineFramingTransposer>();
        _noiseSettings = CM.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    private void Update() { 
         _moveVector = _horizontalPlayerMovement.MoveVector;
        if (_moveVector.x < 0) { CMFT.m_ScreenX = 0.25f; }
        else if (_moveVector.x > 0) { CMFT.m_ScreenX = 0.75f; }
        else CMFT.m_ScreenX = 0.5f; }

    public void Shake()
    {
        StopCoroutine("ShakeCor");
        StartCoroutine(nameof(ShakeCor));
    }

    public IEnumerator ShakeCor()
    {
        _noiseSettings.m_AmplitudeGain = 2;
        yield return new WaitForSeconds(0.15f);
        _noiseSettings.m_AmplitudeGain = 0;
    }


}
