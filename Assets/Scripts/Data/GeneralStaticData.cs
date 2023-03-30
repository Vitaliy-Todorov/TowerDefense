using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GeneralStaticData", menuName = "Data/GeneralStaticData")]
public class GeneralStaticData : ScriptableObject
{
    public List<Color> ColorsOfTile;
    public GameObject BasicPrefabTile;
    public List<GameObject> PrefabTile;
    
    public Stack<Vector2Int> _path;

    public GameObject GetTile(ETileType tileType)
    {
        return PrefabTile[(int) tileType];
    }
}