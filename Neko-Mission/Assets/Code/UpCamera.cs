using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpCamera : MonoBehaviour
{
    public Camera MainCamera;
    public Camera UpCAmera;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            MainCamera.enabled = !MainCamera.enabled;
            UpCAmera.enabled = !UpCAmera.enabled;
            if(UpCAmera.enabled)
                Time.timeScale = 0;
            else Time.timeScale = 1;
        }
    }
}
