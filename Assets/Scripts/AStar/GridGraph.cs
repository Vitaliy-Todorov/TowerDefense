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

    public Stack<Vector2Int> FindingPath(Vector2Int start, Vector2Int finish)
    {
        Stack<Node> currenNodes = new Stack<Node>();
        
        finish += Vector2Int.one;
        Stack<Vector2Int> path = new Stack<Vector2Int>();
        currenNodes.Push(Gride[start.x, start.y]);
        Gride[start.x, start.y].Checked = true;
        
        Stack<Vector2Int> neighborsOfNode = new Stack<Vector2Int>();
        neighborsOfNode.Push(new Vector2Int(1, 1));
        neighborsOfNode.Push(new Vector2Int(1, 0));
        neighborsOfNode.Push(new Vector2Int(1, -1));
        neighborsOfNode.Push(new Vector2Int(0, -1));
        neighborsOfNode.Push(new Vector2Int(-1, -1));
        neighborsOfNode.Push(new Vector2Int(-1, 0));
        neighborsOfNode.Push(new Vector2Int(-1, 1));
        neighborsOfNode.Push(new Vector2Int(0, 1));
        
        float minPathLength;
        (Stack<Node>, float) currenNodesTuple = (currenNodes, float.MaxValue);
        
        while (currenNodes.Peek().Position != finish)
        {
            minPathLength = float.MaxValue;
            
            foreach (Node currenNode in currenNodes)
            {
                currenNodesTuple = FindSuitableNeighbors(finish, neighborsOfNode, currenNode);
                if (minPathLength > currenNodesTuple.Item2)
                {
                    currenNodes = currenNodesTuple.Item1;
                    minPathLength = currenNodesTuple.Item2;
                }
                else if(minPathLength == currenNodesTuple.Item2)
                    foreach (Node currenMinNode in currenNodesTuple.Item1)
                        currenNodes.Push(currenMinNode);
            }
            
            if (currenNodes.Peek().Position == start)
                break;
        }
        
        Node nodeOfPath = currenNodes.Pop();
        
        while (nodeOfPath.Position != start)
        {
            path.Push(nodeOfPath.Position);
            nodeOfPath = nodeOfPath.Parent;
        }
        path.Push(nodeOfPath.Position);
        
        return path;
    }

    private (Stack<Node> minNods, float minPathLength) FindSuitableNeighbors(Vector2Int finish, Stack<Vector2Int> neighborsOfNode, Node currenNode)
    {
        Stack<Node> minNods = new Stack<Node>();
        float minPathLength = float.MaxValue;

        foreach (Vector2Int neighborOfNode in neighborsOfNode)
        {
            Vector2Int positionBeingChecked = currenNode.Position + neighborOfNode;
            if (positionBeingChecked.x < 0
                || positionBeingChecked.y < 0
                || positionBeingChecked.x >= Gride.GetLength(0)
                || positionBeingChecked.y >= Gride.GetLength(1))
                continue;

            Node nodeBeingChecked = Gride[positionBeingChecked.x, positionBeingChecked.y];

            if (nodeBeingChecked.Checked || !nodeBeingChecked.Available)
                continue;
            nodeBeingChecked.Checked = true;

            float movLength = Heuristics(neighborOfNode);
            float pathLengthBeingChecked =
                movLength + currenNode.LengthOfPathPassed + Heuristics(finish - positionBeingChecked);
            if (minPathLength > pathLengthBeingChecked)
            {
                minPathLength = pathLengthBeingChecked;
                minNods.Clear();
                minNods.Push(nodeBeingChecked);
            }
            else if (minPathLength == pathLengthBeingChecked)
            {
                minNods.Push(nodeBeingChecked);
            }
        }

        foreach (Node minNod in minNods)
        {
            minNod.LengthOfPathPassed += Heuristics(minNod.Position - currenNode.Position);
            minNod.Parent = currenNode;
        }

        return (minNods, minPathLength);
    }

    private float Heuristics(Vector2Int v)
    {
        return v.magnitude;
    }
}