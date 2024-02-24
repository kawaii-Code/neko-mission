using UnityEngine;

public class TowerSpawnPlatform : MonoBehaviour
{
    public GameObject Tower;
    public Player Pl;
    public int TowerPrice;
    private bool _towerIsBuilt;

    public void SpawnTower()
    {
        if (!_towerIsBuilt && Pl.CurrentBalance >= TowerPrice)
        {
            Sounds.Play("click1");
            GameObject tower = Instantiate(Tower, transform.position + Vector3.up * Tower.transform.position.y, Quaternion.identity);
            tower.transform.Rotate(Vector3.up, transform.eulerAngles.y + 90f);
            GetComponent<MeshRenderer>().enabled = false;
            
            Pl.CurrentBalance -= TowerPrice;
            _towerIsBuilt = true;
        }

        if (!_towerIsBuilt && Pl.CurrentBalance < TowerPrice)
        {
            Sounds.Play("error1");
        }
    }
}