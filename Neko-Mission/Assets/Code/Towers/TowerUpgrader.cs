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
    public LayerMask TowerLayer;

    private GameObject _selectedTower;
    private bool _upgradeMenuIsOpened;
    bool _towerIsBuiltMenuIsOpened;
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
            if (_upgradeMenuIsOpened || _towerIsBuiltMenuIsOpened)
            {
                Exit();
                return;
            }

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
        Debug.Log("!!!!");
        _selectedTower.GetComponent<BasicTower>().FireRate -= 0.1f;
        Exit();
        OpenTowerIsBuiltMenu();
    }

    public void IncreaseDamage()
    {
        int balance = Pl.CurrentBalance;

        //дочерний элемент, отвечающий за стоимость
        TextMeshProUGUI child = IncreaseDamageButton.GetComponentsInChildren<TextMeshProUGUI>().Where(x => char.IsDigit(x.text[0])).First();

        int price = int.Parse(child.text);
        if (Pl.CurrentBalance >= price)
        {
            Pl.CurrentBalance -= price;
            child.text = (price + 10).ToString();
            Debug.Log("old:" + _selectedTower.GetComponent<BasicTower>().Damage);
            _selectedTower.GetComponent<BasicTower>().Damage += 10000;
            Debug.Log("new:" + _selectedTower.GetComponent<BasicTower>().Damage);
        }
    }

    public void ChangeButtonColor()
    {
        int balance = Pl.CurrentBalance;

        //дочерний элемент, отвечающий за стоимость
        TextMeshProUGUI child = IncreaseDamageButton.GetComponentsInChildren<TextMeshProUGUI>().Where(x => char.IsDigit(x.text[0])).First();
        int price = int.Parse(child.text);
        if (Pl.CurrentBalance < price)
            IncreaseDamageButton.GetComponent<Image>().color = Color.red;
        else
            IncreaseDamageButton.GetComponent<Image>().color = Color.green;

    }

}
