using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(World))]
public class TestAStar : Editor
{
    private static Queue<Vector2Int> _path;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        World world = (World) target;

        _path ??= new Queue<Vector2Int>();
        
        if (GUILayout.Button("FindingPath"))
        {
            GridGraph gridGraph = new GridGraph(10, 10);
            gridGraph.UnavailableNod(1, 1);
            gridGraph.UnavailableNod(1, 2);

            _path = gridGraph.FindingPath(new Vector2Int(0, 0), new Vector2Int(9, 9));
            
            Vector3 oldPosition;
            Vector3 currentPosition = new Vector3(_path.Peek().x, _path.Peek().y);
            foreach (Vector2Int position in _path)
            {
                oldPosition = currentPosition;
                currentPosition = new Vector3(position.x, position.y);
            
                Debug.Log(oldPosition + " -> " + currentPosition);
            }
        }

        EditorUtility.SetDirty(target);
    }

    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected | GizmoType.Selected)]
    public static void RenderCustomGizmo(World world, GizmoType gizmo)
    {
        if(_path == null || _path.Count == 0)
            return;

        Vector3 oldPosition;
        Vector3 currentPosition = new Vector3(_path.Peek().x, _path.Peek().y);
        foreach (Vector2Int position in _path)
        {
            oldPosition = currentPosition;
            currentPosition = new Vector3(position.x, 0, position.y);
            
            Debug.DrawLine(oldPosition, currentPosition, Color.red);
        }
    }
}