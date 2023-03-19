using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuBuildTower : MonoBehaviour
{
    [SerializeField] 
    private List<ButtonTower> _buttonsTowers;

    private BuildTower _buildTower;
    private Vector3 _positionOnGrid;
    private GameObject _currentModelTower;
    private ETowerType _currentTowerType;

    public void Construct(BuildTower buildTower)
    {
        _buildTower = buildTower;

        foreach (ButtonTower buttonTower in _buttonsTowers)
        {
            buttonTower.Construct(this);
            buttonTower.Button.onClick.AddListener(BuildTower);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            CloseMenu();
        }
    }

    private void CloseMenu()
    {
        if (_currentModelTower != null)
            Destroy(_currentModelTower);

        gameObject.SetActive(false);
    }

    public void SetPositionOnGrid(Vector3 position)
    {
        _positionOnGrid = position;
        SetModel((ETowerType) 1);
    }
    
    public void SetModel(ETowerType towerType)
    {
        if(_currentModelTower != null)
            Destroy(_currentModelTower);
        _currentTowerType = towerType;
        _currentModelTower = _buildTower.BuildModel(towerType, _positionOnGrid);
    }

    private void BuildTower()
    {
        _buildTower.Build(_currentTowerType, _positionOnGrid);
        CloseMenu();
    }
}