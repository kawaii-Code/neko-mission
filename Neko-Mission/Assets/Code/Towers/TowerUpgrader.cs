using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Linq;
public class TowerUpgrader : MonoBehaviour
{
    public GameObject TowerIsBuiltMenu;
    public GameObject UpgradeMenu;
    public GameObject Crosshair;
    public PlayerCamera PlayerCamera;
    public Player Pl;
    public PlayerGun Gun;
    public GameObject IncreaseDamageButton;
    public GameObject IncreaseFireRateButton;
    public GameObject AddSlowdownButton;
    public LayerMask TowerLayer;
    public float UpgradeDistance = 10f;
    public bool Paused;
    private Tower _selectedTower;
    private bool _upgradeMenuIsOpened;
    private bool _towerIsBuiltMenuIsOpened;

    void Start()
    {
        TowerIsBuiltMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
        _upgradeMenuIsOpened = false;
        _towerIsBuiltMenuIsOpened = false;
    }

    void Update()
    {
        if (Paused)
            return;

        if (_upgradeMenuIsOpened)
            ChangeButtonColor();

        if (Input.GetMouseButtonDown(1))
        {

            if (_upgradeMenuIsOpened || _towerIsBuiltMenuIsOpened)
            {
                Exit();
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, TowerLayer))
            {
                if (hit.distance > UpgradeDistance)
                {
                    Sounds.Play("error1");
                    return;
                }

                _selectedTower = hit.collider.gameObject.GetComponent<Tower>();

                if (!_upgradeMenuIsOpened)
                    OpenTowerIsBuiltMenu();
            }
        }
    }

    public void ActivateUpgradeMenu()
    {
        Sounds.PlayClick();
        TowerIsBuiltMenu.SetActive(false);
        _towerIsBuiltMenuIsOpened = false;
        UpgradeMenu.SetActive(true);
        _upgradeMenuIsOpened = true;
    }

    public void OpenTowerIsBuiltMenu()
    {
        Sounds.PlayClick();
        Crosshair.SetActive(false);
        TowerIsBuiltMenu.SetActive(true);
        PlayerCamera.Paused = true;
        Gun.Paused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _towerIsBuiltMenuIsOpened = true;
    }

    public void ExitWithoutSound()
    {
        Crosshair.SetActive(true);
        TowerIsBuiltMenu.SetActive(false);
        PlayerCamera.Paused = false;
        Gun.Paused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _towerIsBuiltMenuIsOpened = false;
        _upgradeMenuIsOpened = false;
        UpgradeMenu.SetActive(false);
    }

    public void Exit()
    {
        Sounds.PlayClose();
        ExitWithoutSound();
    }

    public void ReturnToTowerIsBuiltMenu()
    {
        ExitWithoutSound();
        OpenTowerIsBuiltMenu();
    }

    ///увеличение урона башни
    public void IncreaseDamage()
    {
        int price = _selectedTower.IncreaseDamagePrice;
        if (Pl.CurrentBalance >= price)
        {
            Sounds.Play("click1");
            Pl.CurrentBalance -= price;
            _selectedTower.Damage += 1;
            _selectedTower.IncreaseDamagePrice += 10;
        }
        else
            Sounds.Play("error1");
    }

    ///увеличение скорости атаки башни
    public void IncreaseFireRate()
    {
        int price = _selectedTower.IncreaseFireRatePrice;
        if (Pl.CurrentBalance >= price)
        {
            Sounds.Play("click1");
            Pl.CurrentBalance -= price;
            _selectedTower.FireRate -= 0.1f;
            _selectedTower.IncreaseFireRatePrice += 20;
        }
        else
            Sounds.Play("error1");
    }

    ///замедление врагов
    public void AddSlowdown()
    {
        bool isAlreadyAdded = _selectedTower.GetComponent<ShotgunTower>().BulletPrefab.GetComponent<Bullet>().SlowdownIsActivated;
        if (isAlreadyAdded)
        {
            Sounds.Play("error1");
            return;
        }

        int price = _selectedTower.AddSlowdownPrice;
        if (Pl.CurrentBalance >= price)
        {
            Sounds.Play("click1");
            _selectedTower.GetComponent<ShotgunTower>().BulletPrefab.GetComponent<Bullet>().SlowdownIsActivated = true;
            Pl.CurrentBalance -= price;
        }
        else
            Sounds.Play("error1");
    }

    ///изменение цвета кнопок, отвечающих за приобретение улчшения
    public void ChangeButtonColor()
    {
        bool isShotgunTower = _selectedTower.name == "Shotgun Tower(Clone)";
        if (isShotgunTower)
        {
            IncreaseDamageButton.SetActive(false);
            AddSlowdownButton.SetActive(true);
        }

        else
        {
            AddSlowdownButton.SetActive(false);
            IncreaseDamageButton.SetActive(true);
        }

        //кнопка для увеличения урона
        TextMeshProUGUI child = IncreaseDamageButton.GetComponentsInChildren<TextMeshProUGUI>().Where(x => char.IsDigit(x.text[0])).First();
        child.text = _selectedTower.IncreaseDamagePrice.ToString();
        int price = int.Parse(child.text);
        if (Pl.CurrentBalance < price)
            IncreaseDamageButton.GetComponent<Image>().color = Color.red;
        else
            IncreaseDamageButton.GetComponent<Image>().color = Color.green;

        // кнопка для увеличения скорости стрельбы башни
        child = IncreaseFireRateButton.GetComponentsInChildren<TextMeshProUGUI>().Where(x => char.IsDigit(x.text[0])).First();
        child.text = _selectedTower.IncreaseFireRatePrice.ToString();
        price = int.Parse(child.text);
        if (Pl.CurrentBalance < price)
            IncreaseFireRateButton.GetComponent<Image>().color = Color.red;
        else
            IncreaseFireRateButton.GetComponent<Image>().color = Color.green;

        if (!isShotgunTower)
            return;


        //кнопка для добавления замедления врагов
        child = AddSlowdownButton.GetComponentsInChildren<TextMeshProUGUI>().Where(x => char.IsDigit(x.text[0])).First();
        child.text = _selectedTower.GetComponent<ShotgunTower>().AddSlowdownPrice.ToString();
        price = int.Parse(child.text);

        if (_selectedTower.GetComponent<ShotgunTower>().BulletPrefab.GetComponent<Bullet>().SlowdownIsActivated)
        {
            AddSlowdownButton.GetComponent<Image>().color = Color.gray;
            return;
        }

        if (Pl.CurrentBalance < price)
            AddSlowdownButton.GetComponent<Image>().color = Color.red;
        else
            AddSlowdownButton.GetComponent<Image>().color = Color.green;

    }
}
