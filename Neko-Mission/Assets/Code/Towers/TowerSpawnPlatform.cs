using UnityEngine;

public class TowerSpawnPlatform : MonoBehaviour
{
    public GameObject Tower;
    public Player Pl;

    public int _TowerPrice;
    private bool _towerIsBuilt;

    public void SpawnTower()
    {
        _TowerPrice = Tower.GetComponent<Tower>().Price;

        if (!_towerIsBuilt && Pl.CurrentBalance >= _TowerPrice)
        {
            Sounds.Play("click1");
            GameObject tower = Instantiate(Tower, transform.position + Vector3.up * Tower.transform.position.y, Quaternion.identity);
            tower.transform.Rotate(Vector3.up, transform.eulerAngles.y);
            GetComponent<MeshRenderer>().enabled = false;
            
            Pl.CurrentBalance -= _TowerPrice;
            _towerIsBuilt = true;
            gameObject.SetActive(false);
        }

        if (!_towerIsBuilt && Pl.CurrentBalance < _TowerPrice)
        {
            Sounds.PlayClose();
        }
    }
}