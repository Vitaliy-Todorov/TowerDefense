using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(LevelData))]
public class GridEditor : Editor
{
    private const string GeneralStaticDataPath = "Data/GeneralStaticData";
    private static GeneralStaticData _generalStaticData;
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelData levelData = (LevelData) target;

        if (GUILayout.Button("Create a new grid"))
        {
            levelData.NameScene = SceneManager.GetActiveScene().name;
            
            if (_generalStaticData == null)
                _generalStaticData = Resources.Load<GeneralStaticData>(GeneralStaticDataPath);

            if (levelData.GridGO == null) 
                levelData.GridGO = new GameObject("Grid");

            if (levelData.Grid == null)
                levelData.Grid = new TileSpawnMarker[levelData.GridSize.x, levelData.GridSize.y];

            foreach (TileSpawnMarker tileSpawnMarker in levelData.Grid) 
                if(tileSpawnMarker != null)
                    DestroyImmediate(tileSpawnMarker.gameObject);

            if (levelData.Grid.GetLength(0) != levelData.GridSize.x 
                && levelData.Grid.GetLength(0) != levelData.GridSize.y)
                levelData.Grid = new TileSpawnMarker[levelData.GridSize.x, levelData.GridSize.y];

            for (int x = 0; x < levelData.GridSize.x; x++)
                for (int y = 0; y < levelData.GridSize.y; y++)
                {
                    GameObject newTile = Instantiate(_generalStaticData.BasicPrefabTile);
                    
                    levelData.Grid[x,y] = newTile.AddComponent<TileSpawnMarker>();
                    levelData.Grid[x, y].Position = new Vector2Int(x, y);
                    
                    newTile.transform.SetParent(levelData.GridGO.transform);
                    
                    newTile.name = $"Tile({x},{y})";
                    newTile.transform.position = new Vector3(x, 0, y);
                }
                
            EditorUtility.SetDirty(target);
        }
    }
}