using UnityEditor;
using UnityEngine;

public class TileSpawnerMarkerEditor : Editor
{
    private const string GeneralStaticDataPath = "Data/GeneralStaticData";
    private static GeneralStaticData _generalStaticData;
    
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected | GizmoType.Selected)]
    public static void RenderCustomGizmo(TileSpawnMarker tileSpawnMarker, GizmoType gizmo)
    {
        if (_generalStaticData == null)
            _generalStaticData = Resources.Load<GeneralStaticData>(GeneralStaticDataPath);

        Gizmos.color = _generalStaticData.ColorsOfTile[(int) tileSpawnMarker.TileType];
        tileSpawnMarker.transform.position = new Vector3(tileSpawnMarker.Position.x, 0, tileSpawnMarker.Position.y);
        Gizmos.DrawCube(tileSpawnMarker.transform.position, new Vector3(1, .1f, 1));
    }
}