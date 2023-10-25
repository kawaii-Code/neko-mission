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
            Instantiate(Tower, transform.position + Vector3.up * Tower.transform.localScale.y / 2, Quaternion.identity);
            _towerIsBuilt = true;
        }
    }

}
