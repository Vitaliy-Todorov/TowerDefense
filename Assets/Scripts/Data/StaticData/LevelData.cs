using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData")]
public class LevelData : ScriptableObject
{
    public string NameScene;
    public string NameLevel;
    
    public Vector2Int GridSize;
    public TileMarker[,] Grid;
    public GameObject GridGO;
}