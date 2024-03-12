using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpCamera : MonoBehaviour
{
    public Camera MainCamera;
    public Camera UpCAmera;
    public Image Crosshair;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Crosshair.enabled = !Crosshair.enabled;
            MainCamera.enabled = !MainCamera.enabled;
            UpCAmera.enabled = !UpCAmera.enabled;
            if(UpCAmera.enabled)
                Time.timeScale = 0;
            else Time.timeScale = 1;
        }
    }
}
