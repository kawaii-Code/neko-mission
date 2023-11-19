using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Tower;
    public Player Pl;
    public int TowerPrice;
    private bool _towerIsBuilt = false;

    public void OnMouseDown()
    {
        if (!_towerIsBuilt && Pl.CurrentBalance >= TowerPrice)
        {
            Instantiate(Tower, transform.position + Vector3.up * Tower.transform.localScale.y / 2, Quaternion.identity);
            Pl.CurrentBalance -= TowerPrice;
            _towerIsBuilt = true;
        }

        if (!_towerIsBuilt && Pl.CurrentBalance < TowerPrice)
        {
            Debug.Log("Недостаточно денег!");
        }
    }
}