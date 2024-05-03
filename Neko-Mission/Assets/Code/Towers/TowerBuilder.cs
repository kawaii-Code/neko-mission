using TMPro;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    public LayerMask TowerSpawnerLayer;
    public LayerMask TowerLayer;

    public float BuildDistance = 10f;

    public bool Paused;

    public GameObject Crosshair;
    public GameObject TowerPrefab1;
    public GameObject TowerPrefab2;
    public GameObject SelectMenu;
    public GameObject TowerSpeedUpMenu;

    public RaycastHit _LastHitInfo;

    public PlayerGun Gun;
    public PlayerCamera PlayerCamera;

    private bool _buildMenuShown = false;
    private bool _speedUpMenuShown = false;
    private Tower SpeedUpTower;


    void Start()
    {
        TowerSpeedUpMenu.SetActive(false);
        SelectMenu.SetActive(false);
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
                Sounds.PlayClose();
                return;
            }
            
            if (_speedUpMenuShown) {
                DisableTowerSpeedUpMenu();
                Sounds.PlayClose();
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
                return;
            }

            if (Physics.Raycast(ray, out RaycastHit hitInfo2, float.MaxValue, TowerLayer)) {

                if (hitInfo2.distance > BuildDistance) 
                {
                    Sounds.Play("error1");
                    return;
                }
                _LastHitInfo = hitInfo2;

                Tower _tower = _LastHitInfo.transform.GetComponent<Tower>();

                if (_tower.HasBonusSpeed()) TowerSpeedUpMenu.GetComponentInChildren<TextMeshProUGUI>().text = "Снять ускорение с башни";
                else TowerSpeedUpMenu.GetComponentInChildren<TextMeshProUGUI>().text = "Ускорить башню";

                EnableTowerSpeedUpMenu();
                return;
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

    public void TowerSpeedUp() {
        Tower tower = _LastHitInfo.transform.GetComponent<Tower>();
        if (!tower.HasBonusSpeed()) {
            if (SpeedUpTower != null) SpeedUpTower.SlowdownFireRate();

            tower.SpeedUpFireRate();
            SpeedUpTower = tower;
        }
        else { 
            tower.SlowdownFireRate();
            SpeedUpTower = null;
        }
        DisableTowerSpeedUpMenu();
    }

    public void EnableBuildMenu()
    {
        Sounds.PlayClick();

        SelectMenu.SetActive(true);
        _buildMenuShown = true;

        PlayerCamera.Paused = true;
        Gun.Paused = true;

        Crosshair.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisableBuildMenu()
    {
        SelectMenu.SetActive(false);
        _buildMenuShown = false;

        PlayerCamera.Paused = false;
        Gun.Paused = false;

        Crosshair.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void EnableTowerSpeedUpMenu() {
        Sounds.PlayClick();

        TowerSpeedUpMenu.SetActive(true);
        _speedUpMenuShown = true;

        PlayerCamera.Paused = true;
        Gun.Paused = true;

        Crosshair.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisableTowerSpeedUpMenu() {
        TowerSpeedUpMenu.SetActive(false);
        _speedUpMenuShown = false;

        PlayerCamera.Paused = false;
        Gun.Paused = false;

        Crosshair.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}