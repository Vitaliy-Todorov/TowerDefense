using System.Collections.Generic;
using UnityEngine;

public class TestAStar : MonoBehaviour
{
    public GridGraph grid;
    private static Stack<Vector2Int> _path;
    private static List<Vector2Int> _pathList;

    public void Start()
    {
        _path ??= new Stack<Vector2Int>();
        
        grid.Construct();

        _path = grid._FindingPath(new Vector2Int(20, 20), new Vector2Int(9, 9));

        if(_path.Count == 0) return;

        _pathList = new List<Vector2Int>();
        Vector3 oldPosition;
        Vector3 currentPosition = new Vector3(_path.Peek().x, 0, _path.Peek().y);
        Debug.Log(_path.Count);
        foreach (Vector2Int position in _path)
        {
            _pathList.Add(position);
            oldPosition = currentPosition;
            currentPosition = new Vector3(position.x, position.y);
            
            Debug.Log(oldPosition + " -> " + currentPosition);
        }
    }

    public void Update()
    {
        if(_path == null || _path.Count == 0)
            return;
        
        Vector3 oldPosition;
        Vector3 currentPosition = new Vector3(_pathList[0].x, 0, _pathList[0].y);
        //foreach (Vector2Int position in _path)
        for (int i = 1; i < _pathList.Count; i++)
        {
            oldPosition = currentPosition;
            currentPosition = new Vector3(_pathList[i].x, 0, _pathList[i].y);
            
            Debug.DrawLine(oldPosition, currentPosition, Color.red);
        }
    }
}