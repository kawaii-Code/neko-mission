using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    public LayerMask TowerSpawnerLayer;
    public float BuildDistance = 10f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, TowerSpawnerLayer))
            {
                if (hitInfo.distance > BuildDistance)
                {
                    Sounds.Play("error1");
                    return;
                }

                TowerSpawnPlatform spawnPlatform = hitInfo.transform.GetComponent<TowerSpawnPlatform>();
                spawnPlatform.SpawnTower();
            }
        }
    }
}