using UnityEngine;

public class TowerUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform AttackRadiusUI;
    
    public void Construct(TowerData towerData)
    {
        AttackRadiusUI.sizeDelta = Vector2.one * towerData.AttackRadius * 2;
    }
}
