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
    public Button IncreaseDamageButton;
    public Button IncreaseFireRateButton;
    public LayerMask TowerLayer;

    private GameObject _selectedTower;
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
        if (_upgradeMenuIsOpened)
            ChangeButtonColor();

        if (Input.GetMouseButton(1))
        {
            // if (_upgradeMenuIsOpened || _towerIsBuiltMenuIsOpened)
            //{
            //  Exit();
            //return;
            //}
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, TowerLayer))
            {
                _selectedTower = hit.collider.gameObject;
                Debug.Log(_selectedTower.gameObject.name + " " + _selectedTower.layer);

                OpenTowerIsBuiltMenu();
            }
        }
    }

    public void ActivateUpgradeMenu()
    {
        TowerIsBuiltMenu.SetActive(false);
        _towerIsBuiltMenuIsOpened = false;
        UpgradeMenu.SetActive(true);
        _upgradeMenuIsOpened = true;
    }

    public void OpenTowerIsBuiltMenu()
    {
        Crosshair.SetActive(false);
        TowerIsBuiltMenu.SetActive(true);
        PlayerCamera.Paused = true;
        Gun.Paused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _towerIsBuiltMenuIsOpened = true;
    }

    public void Exit()
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

    public void ReturnToTowerIsBuiltMenu()
    {
        Exit();
        OpenTowerIsBuiltMenu();
    }

    ///увеличение урона башни
    public void IncreaseDamage()
    {
        int price = _selectedTower.GetComponent<Tower>().IncreaseDamagePrice;
        if (Pl.CurrentBalance >= price)
        {
            Pl.CurrentBalance -= price;
            _selectedTower.GetComponent<Tower>().Damage += 10000;
            _selectedTower.GetComponent<Tower>().IncreaseDamagePrice += 10;
        }
    }

    ///увеличение скорости атаки башни
    public void IncreaseFireRate()
    {
        int price = _selectedTower.GetComponent<Tower>().IncreaseFireRatePrice;
        if (Pl.CurrentBalance >= price)
        {
            Pl.CurrentBalance -= price;
            _selectedTower.GetComponent<Tower>().FireRate -= 0.1f;
            _selectedTower.GetComponent<Tower>().IncreaseFireRatePrice += 20;
        }
    }

    ///изменение цвета кнопок, отвечающих за приобретение улчшения
    public void ChangeButtonColor()
    {
        int balance = Pl.CurrentBalance;
        //кнопка для увеличения урона
        TextMeshProUGUI child = IncreaseDamageButton.GetComponentsInChildren<TextMeshProUGUI>().Where(x => char.IsDigit(x.text[0])).First();
        child.text = (_selectedTower.GetComponent<Tower>().IncreaseDamagePrice).ToString();
        int price = int.Parse(child.text);
        if (Pl.CurrentBalance < price)
            IncreaseDamageButton.GetComponent<Image>().color = Color.red;
        else
            IncreaseDamageButton.GetComponent<Image>().color = Color.green;

        // кнопка для увеличения скорости стрельбы башни
        child = IncreaseFireRateButton.GetComponentsInChildren<TextMeshProUGUI>().Where(x => char.IsDigit(x.text[0])).First();
        child.text = (_selectedTower.GetComponent<Tower>().IncreaseFireRatePrice).ToString();
        price = int.Parse(child.text);
        if (Pl.CurrentBalance < price)
            IncreaseFireRateButton.GetComponent<Image>().color = Color.red;
        else
            IncreaseFireRateButton.GetComponent<Image>().color = Color.green;
    }
}
