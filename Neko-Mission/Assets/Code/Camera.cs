using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Camera : MonoBehaviour
{
    public TMP_Text SensitivityValue;

    public float MinPitch = -60f;
    public float MaxPitch = 60f;
    
    public float Sensitivity = 1f;
    public Transform Player;

    private float _pitch;
    private float _yaw;

    public void ChangeSensitivity(Slider slider)
    {
        Sensitivity = Mathf.Lerp(0.0f, 10, slider.value);
        SensitivityValue.text = $"{Sensitivity}";
    }
    
    private void Update()
    {
        float rotationX = Input.GetAxis("Mouse X");
        float rotationY = Input.GetAxis("Mouse Y");

        _pitch = Mathf.Clamp(_pitch - Sensitivity * rotationY, MinPitch, MaxPitch);
        _yaw += Sensitivity * rotationX;

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
