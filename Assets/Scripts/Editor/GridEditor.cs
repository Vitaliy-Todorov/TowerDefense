using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridGraph))]
public class GridEditor : Editor
{
    private const string GeneralStaticDataPath = "Data/GeneralStaticData";
    private static GeneralStaticData _generalStaticData;
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GridGraph grid = (GridGraph) target;
        
        if (GUILayout.Button("Create a new grid"))
        {
            // levelData.NameScene = SceneManager.GetActiveScene().name;
            
            if (_generalStaticData == null)
                _generalStaticData = Resources.Load<GeneralStaticData>(GeneralStaticDataPath);

            if (grid.TileGrid == null)
                grid.TileGrid = new TileMarker[grid.Size.x, grid.Size.y];
            
            foreach (TileMarker tileMarker in grid.TileGrid) 
                if(tileMarker != null)
                    DestroyImmediate(tileMarker.gameObject);
            
            UpdateSizeGrid(grid);

            for (int x = 0; x < grid.Size.x; x++)
                for (int y = 0; y < grid.Size.y; y++)
                {
                    GameObject newTile = Instantiate(_generalStaticData.BasicPrefabTile);

                    grid.TileGrid[x,y] = newTile.GetComponent<TileMarker>();
                    grid.TileGrid[x, y].Position = new Vector2Int(x, y);
                    
                    newTile.transform.SetParent(grid.transform);
                    
                    newTile.name = $"Tile({x},{y})";
                    newTile.transform.position = new Vector3(x, 0, y);
                }
                
            EditorUtility.SetDirty(target);
        }

        if (GUILayout.Button("Update the grid"))
        {
            TileMarker[] tileGrid = grid.GetComponentsInChildren<TileMarker>();
            
            if(grid.TileGrid == null)
                grid.TileGrid = new TileMarker[grid.Size.x, grid.Size.y];
            
            UpdateSizeGrid(grid);
            
            foreach (TileMarker tileMarker in tileGrid) 
                grid.TileGrid[tileMarker.Position.x, tileMarker.Position.y] = tileMarker;
        }
    }

    private static void UpdateSizeGrid(GridGraph grid)
    {
        if (grid.TileGrid.GetLength(0) != grid.Size.x
            && grid.TileGrid.GetLength(0) != grid.Size.y)
            grid.TileGrid = new TileMarker[grid.Size.x, grid.Size.y];
    }
}