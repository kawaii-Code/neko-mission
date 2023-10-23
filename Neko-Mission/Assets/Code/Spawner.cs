using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject Tower;
    private bool _towerIsBuilt = false;

    public void OnMouseDown()
    {
        if (!_towerIsBuilt)
        {
            Instantiate(Tower, transform.position + Vector3.up * 3.0f, Quaternion.identity);
            _towerIsBuilt = true;
        }
    }

    void Update()
    {

    }
}
