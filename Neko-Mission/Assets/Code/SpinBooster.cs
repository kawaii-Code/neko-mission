using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinBooster : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0 ,0 ,25 * Time.deltaTime);
    }
}
