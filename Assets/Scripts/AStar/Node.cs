using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public readonly Vector2Int Position;
    public bool Available;
    public bool Checked;
    public Stack<Node> AvailableNeighbors;
    public Node Parent;
    public float LengthOfPathPassed;
    public float EstimatedOfAllPath;
    
    public Node(int x, int y)
    {
        Position = new Vector2Int(x, y);
        Available = true;
        LengthOfPathPassed = 0;
        EstimatedOfAllPath = float.MaxValue;
    }

    public void Clear()
    {
        Parent = null;
        LengthOfPathPassed = 0;
        EstimatedOfAllPath = 0;
    }
}