using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Tower;
<<<<<<< Updated upstream
=======
    public Player Pl;
>>>>>>> Stashed changes
    public int TowerPrice;
    private bool _towerIsBuilt = false;

    public void OnMouseDown()
    {
<<<<<<< Updated upstream
        if (!_towerIsBuilt && Player.CurrentBalance >= TowerPrice)
        {
            Instantiate(Tower, transform.position + Vector3.up * Tower.transform.localScale.y / 2, Quaternion.identity);
            Player.CurrentBalance -= TowerPrice;
            _towerIsBuilt = true;
        }
    }
}
=======
        if (!_towerIsBuilt && Pl.CurrentBalance >= TowerPrice)
        {
            Instantiate(Tower, transform.position + Vector3.up * Tower.transform.localScale.y / 2, Quaternion.identity);
            Pl.CurrentBalance -= TowerPrice;
            _towerIsBuilt = true;
        }
    }
}
>>>>>>> Stashed changes
