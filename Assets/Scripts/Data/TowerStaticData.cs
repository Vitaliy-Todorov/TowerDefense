using UnityEngine;

[CreateAssetMenu(fileName = "TowerStaticData", menuName = "Data/TowerStaticData")]
public class TowerStaticData : ScriptableObject
{
    public ETowerType Type;
    public GameObject Prefab;
    public GameObject PrefabModel;

    public TowerData Data;
}