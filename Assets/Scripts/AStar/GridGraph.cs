using System.Collections.Generic;
using UnityEngine;

public class GridGraph
{
    private Node[,] Gride;

    public GridGraph(int sizeX, int sizeY)
    {
        sizeX += 1;
        sizeY += 1;
        
        Gride = new Node[sizeX, sizeY];
        
        for (int x = 0; x < sizeX; x++)
        for (int y = 0; y < sizeY; y++)
            Gride[x, y] = new Node(x, y);
    }

    public void UnavailableNod(int x, int y) => 
        Gride[x, y].Available = false;

    public Queue<Vector2Int> FindingPath(Vector2Int start, Vector2Int finish)
    {
        finish += Vector2Int.one;
        Queue<Vector2Int> path = new Queue<Vector2Int>();
        Node currenNode = Gride[start.x, start.y];
        path.Enqueue(currenNode.Position);
        currenNode.Checked = true;

        Stack<Vector2Int> neighborsOfNode = new Stack<Vector2Int>();
        neighborsOfNode.Push(new Vector2Int(1, 1));
        neighborsOfNode.Push(new Vector2Int(1, 0));
        neighborsOfNode.Push(new Vector2Int(1, -1));
        neighborsOfNode.Push(new Vector2Int(0, -1));
        neighborsOfNode.Push(new Vector2Int(-1, -1));
        neighborsOfNode.Push(new Vector2Int(-1, 0));
        neighborsOfNode.Push(new Vector2Int(-1, 1));
        neighborsOfNode.Push(new Vector2Int(0, 1));

        while (currenNode.Position != finish)
        {
            float minPathLength = float.MaxValue;
            Node minNod = null;

            foreach (Vector2Int neighborOfNode in neighborsOfNode)
            {
                Vector2Int positionBeingChecked = currenNode.Position + neighborOfNode;
                if (positionBeingChecked.x < 0
                    || positionBeingChecked.y < 0
                    || positionBeingChecked.x >= Gride.GetLength(0)
                    || positionBeingChecked.y >= Gride.GetLength(1))
                    continue;
                
                Node nodeBeingChecked = Gride[positionBeingChecked.x, positionBeingChecked.y];
                
                if(nodeBeingChecked.Checked || !nodeBeingChecked.Available)
                    continue;
                nodeBeingChecked.Checked = true;
                
                float movLength = Heuristics(neighborOfNode);
                float pathLengthBeingChecked =
                    movLength + currenNode.LengthOfPathPassed + Heuristics(finish - positionBeingChecked);
                if (minPathLength > pathLengthBeingChecked)
                {
                    minPathLength = pathLengthBeingChecked;
                    minNod = nodeBeingChecked;
                }
            }

            minNod.LengthOfPathPassed += Heuristics(minNod.Position - currenNode.Position);
            minNod.Parent = currenNode;
            currenNode = minNod;
            
            path.Enqueue(currenNode.Position);
        }

        return path;
    }

    private float Heuristics(Vector2Int v)
    {
        return v.magnitude;
    }
}

public class Node
{
    public readonly Vector2Int Position;
    public bool Available;
    public bool Checked;
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
