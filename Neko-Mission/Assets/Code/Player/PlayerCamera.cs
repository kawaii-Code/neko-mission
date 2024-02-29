using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float MinPitch = -60f;
    public float MaxPitch = 60f;

    public Transform Player;

    private SettingValue<float> _sensitivity;
    private float _pitch;
    private float _yaw;

    public bool Paused;

    public Camera MainCamera;

    private void Awake()
    {
        _sensitivity = Settings.CameraSensitivity;
    }

    private void Update()
    {
        if (Paused || !MainCamera.enabled)
            return;

        Console.WriteLine(_sensitivity);

        float rotationX = Input.GetAxis("Mouse X");
        float rotationY = Input.GetAxis("Mouse Y");

        _pitch = Mathf.Clamp(_pitch - _sensitivity * rotationY, MinPitch, MaxPitch);
        _yaw += _sensitivity * rotationX;

        Vector3 playerRotation = Player.rotation.eulerAngles;
        playerRotation.y = _yaw;
        Player.rotation = Quaternion.Euler(playerRotation);
    }

    private void LateUpdate()
    {
        transform.position = Player.position;
        transform.rotation = Quaternion.Euler(_pitch, _yaw, 0.0f);
    }
}
