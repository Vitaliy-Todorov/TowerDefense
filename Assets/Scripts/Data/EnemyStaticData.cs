using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStaticData", menuName = "Data/EnemyStaticData")]
public class EnemyStaticData : ScriptableObject
{
    public EEnemyType Type;
    public GameObject Prefab;

    public EnemyData Data;
}