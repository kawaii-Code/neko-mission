using UnityEditor.PackageManager;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    public LayerMask TowerSpawnerLayer;
    public float BuildDistance = 10f;

    public bool Paused;

    public GameObject TowerPrefab1;
    public GameObject TowerPrefab2;
    public GameObject Select_Menu;

    public RaycastHit _LastHitInfo;

    public PlayerCamera PlayerCamera;

    private bool _buildMenuShown;

    void Start()
    {
        Select_Menu.SetActive(false);
    }

    private void Update()
    {
        if (Paused)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            if (_buildMenuShown)
            {
                DisableBuildMenu();
                return;
            }
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, TowerSpawnerLayer))
            {
                if (hitInfo.distance > BuildDistance)
                {
                    Sounds.Play("error1");
                    return;
                }
                _LastHitInfo = hitInfo;
                
                EnableBuildMenu();
            }
        }
    }

    public void Tower1()
    {
        TowerSpawnPlatform spawnPlatform = _LastHitInfo.transform.GetComponent<TowerSpawnPlatform>();

        spawnPlatform.Tower = TowerPrefab1;

        spawnPlatform.SpawnTower();

        DisableBuildMenu();
    }

    public void Tower2()
    {
        TowerSpawnPlatform spawnPlatform = _LastHitInfo.transform.GetComponent<TowerSpawnPlatform>();

        spawnPlatform.Tower = TowerPrefab2;

        spawnPlatform.SpawnTower();

        DisableBuildMenu();
    }

    public void EnableBuildMenu()
    {
        PlayerCamera.Paused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Select_Menu.SetActive(true);
        _buildMenuShown = true;
    }

    public void DisableBuildMenu()
    {
        _buildMenuShown = false;
        PlayerCamera.Paused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Select_Menu.SetActive(false);
    }
}