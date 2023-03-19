using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTower : MonoBehaviour, IPointerEnterHandler
{
    private MenuBuildTower _menuBuildTower;
    
    public ETowerType TowerType;
    public Button Button;

    public void Construct(MenuBuildTower menuBuildTower)
    {
        _menuBuildTower = menuBuildTower;
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        _menuBuildTower.SetModel(TowerType);
    }
}