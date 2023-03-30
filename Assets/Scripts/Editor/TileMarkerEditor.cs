using UnityEditor;
using UnityEngine;

public class TileMarkerEditor : Editor
{
    private const string GeneralStaticDataPath = "Data/GeneralStaticData";
    private static GeneralStaticData _generalStaticData;
    
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected | GizmoType.Selected)]
    public static void RenderCustomGizmo(TileMarker tileMarker, GizmoType gizmo)
    {
        if (_generalStaticData == null)
            _generalStaticData = Resources.Load<GeneralStaticData>(GeneralStaticDataPath);

        Gizmos.color = _generalStaticData.ColorsOfTile[(int) tileMarker.TileType];
        tileMarker.transform.position = new Vector3(tileMarker.Position.x, 0, tileMarker.Position.y);
        Gizmos.DrawCube(tileMarker.transform.position, new Vector3(1, .1f, 1));
    }
}