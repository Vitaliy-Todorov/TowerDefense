using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GeneralStaticData", menuName = "Data/GeneralStaticData")]
public class GeneralStaticData : ScriptableObject
{
    public List<Color> ColorsOfTile;
    public GameObject BasicPrefabTile;
    
    
    public Stack<Vector2Int> _path;
}