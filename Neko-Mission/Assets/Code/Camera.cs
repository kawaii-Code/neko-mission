using UnityEngine;

public class Camera : MonoBehaviour
{
    public float Sensitivity = 1f;
    public Transform Player;

    private Vector3 _newRotation;
    
    private void Update()
    {
        float rotationX = Input.GetAxis("Mouse X");
        float rotationY = Input.GetAxis("Mouse Y");

        Vector3 previousRotation = transform.rotation.eulerAngles;
        Vector3 delta = new(-1 * Sensitivity * rotationY, Sensitivity * rotationX);
        
        _newRotation = previousRotation + delta;

        Vector3 playerRotation = Player.rotation.eulerAngles;
        playerRotation.y = _newRotation.y;
        Player.rotation = Quaternion.Euler(playerRotation);
    }

    private void LateUpdate()
    {
        transform.position = Player.position;
        transform.rotation = Quaternion.Euler(_newRotation);
    }
}
