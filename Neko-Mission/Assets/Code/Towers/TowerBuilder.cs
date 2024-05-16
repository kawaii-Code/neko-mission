using TMPro;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    public LayerMask TowerSpawnerLayer;
    public LayerMask TowerLayer;
    // Ну, уже игра сделана, можно чуть-чуть погрязнокодить 😁
    public TowerUpgrader TowerUpgrader;

    public float BuildDistance = 10f;

    public bool Paused;

    public GameObject Crosshair;
    public GameObject TowerPrefab1;
    public GameObject TowerPrefab2;
    public GameObject BuildMenu;
    public GameObject TowerSpeedUpText;

    public RaycastHit _LastHitInfo;

    public PlayerGun Gun;
    public PlayerCamera PlayerCamera;

    private bool _buildMenuShown = false;
    private Tower SpeedUpTower;


    void Start()
    {
        BuildMenu.SetActive(false);
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

            if (TowerUpgrader.UpgradeMenuIsOpened) {
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

                if (_tower.HasBonusSpeed()) TowerSpeedUpText.GetComponentInChildren<TextMeshProUGUI>().text = "Снять ускорение с башни";
                else TowerSpeedUpText.GetComponentInChildren<TextMeshProUGUI>().text = "Ускорить башню";

                EnableTowerSpeedUpMenu(_tower);
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
        Sounds.Play("jump");
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

        BuildMenu.SetActive(true);
        _buildMenuShown = true;

        PlayerCamera.Paused = true;
        Gun.Paused = true;

        Crosshair.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisableBuildMenu()
    {
        BuildMenu.SetActive(false);
        _buildMenuShown = false;

        PlayerCamera.Paused = false;
        Gun.Paused = false;

        Crosshair.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void EnableTowerSpeedUpMenu(Tower selectedTower)
    {
        TowerUpgrader.UpgradeMenuIsOpened = true;
        TowerUpgrader.SelectedTower = selectedTower;
        TowerUpgrader.OpenTowerIsBuiltMenu();
    }

    public void DisableTowerSpeedUpMenu()
    {
        TowerUpgrader.UpgradeMenuIsOpened = false;
        TowerUpgrader.Exit();
    }
}
