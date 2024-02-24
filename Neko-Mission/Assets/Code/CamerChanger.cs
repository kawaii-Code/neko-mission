using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChengers : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] virtualCameras;
    private int _currentCameraIndex;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SwitchCamera();
        }
    }
    private void SwitchCamera()
    {
        virtualCameras[_currentCameraIndex].gameObject.SetActive(false);
        _currentCameraIndex++;

        if(_currentCameraIndex >= virtualCameras.Length)
        {
            _currentCameraIndex = 0;
        }

        virtualCameras[_currentCameraIndex].gameObject.SetActive(true);
    }
}