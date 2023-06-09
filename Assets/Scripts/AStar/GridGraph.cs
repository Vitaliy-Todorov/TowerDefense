using System.Collections.Generic;
using UnityEngine;

public class GridGraph
{
    private Node[,] Gride;
    private Stack<Vector2Int> _neighborsOfNode;

    public GridGraph(int sizeX, int sizeY)
    {
        sizeX += 1;
        sizeY += 1;
        
        Gride = new Node[sizeX, sizeY];
        
        for (int x = 0; x < sizeX; x++)
        for (int y = 0; y < sizeY; y++)
            Gride[x, y] = new Node(x, y);
        
        _neighborsOfNode = new Stack<Vector2Int>();
        _neighborsOfNode.Push(new Vector2Int(1, 1));
        _neighborsOfNode.Push(new Vector2Int(1, 0));
        _neighborsOfNode.Push(new Vector2Int(1, -1));
        _neighborsOfNode.Push(new Vector2Int(0, -1));
        _neighborsOfNode.Push(new Vector2Int(-1, -1));
        _neighborsOfNode.Push(new Vector2Int(-1, 0));
        _neighborsOfNode.Push(new Vector2Int(-1, 1));
        _neighborsOfNode.Push(new Vector2Int(0, 1));
    }

    public void UnavailableNod(int x, int y) => 
        Gride[x, y].Available = false;

    public Stack<Vector2Int> FindingPath(Vector2Int start, Vector2Int finish)
    {
        finish += Vector2Int.one;
        Stack<Vector2Int> path = new Stack<Vector2Int>();
        
        Stack<Node> currenNodes = new Stack<Node>();
        currenNodes.Push(Gride[start.x, start.y]);
        Gride[start.x, start.y].Checked = true;
        
        float minPathLength;
        (Stack<Node>, float) currenNodesTuple = (currenNodes, float.MaxValue);
        
        while (currenNodes.Peek().Position != finish)
        {
            minPathLength = float.MaxValue;

            Stack<Node> nextNodes = new Stack<Node>();
            
            foreach (Node currenNode in currenNodes)
            {
                currenNodesTuple = FindSuitableNeighbors(finish, currenNode);
                if (minPathLength > currenNodesTuple.Item2)
                {
                    nextNodes = currenNodesTuple.Item1;
                    minPathLength = currenNodesTuple.Item2;
                }
                else if(minPathLength == currenNodesTuple.Item2)
                    foreach (Node currenMinNode in currenNodesTuple.Item1)
                        nextNodes.Push(currenMinNode);
            }

            currenNodes = nextNodes;
            
            if (currenNodes.Peek().Position == start && currenNodes.Count == 0)
                break;
        }
        
        path = ReconstructPath(start, currenNodes);

        return path;
    }

    private (Stack<Node> minNods, float minPathLength) FindSuitableNeighbors(Vector2Int finish, Node currenNode)
    {
        if (currenNode.AvailableNeighbors != null)
            return FindSuitable(currenNode);
        else
            return FindNeighborsAndFindSuitable(finish, currenNode);
    }

    private (Stack<Node> minNods, float minPathLength) FindSuitable(Node currenNode)
    {
        Stack<Node> minNods = new Stack<Node>();
        float minPathLength = float.MaxValue;
        foreach (Node nodeBeingChecked in currenNode.AvailableNeighbors)
            MinPathLength(ref minPathLength, nodeBeingChecked, minNods);
        return (minNods, minPathLength);
    }

    private (Stack<Node> minNods, float minPathLength) FindNeighborsAndFindSuitable(Vector2Int finish, Node currenNode)
    {
        Stack<Node> minNods = new Stack<Node>();
        float minPathLength = float.MaxValue;
        
        currenNode.AvailableNeighbors = new Stack<Node>();
        foreach (Vector2Int neighborOfNode in _neighborsOfNode)
        {
            Vector2Int positionBeingChecked = currenNode.Position + neighborOfNode;
            if (NodeInGrid(positionBeingChecked))
                continue;

            Node nodeBeingChecked = Gride[positionBeingChecked.x, positionBeingChecked.y];

            if (nodeBeingChecked.Checked || !nodeBeingChecked.Available)
                continue;

            nodeBeingChecked.LengthOfPathPassed += Heuristics(neighborOfNode);
            nodeBeingChecked.EstimatedOfAllPath = currenNode.LengthOfPathPassed + Heuristics(finish - positionBeingChecked);
            nodeBeingChecked.Parent = currenNode;
            nodeBeingChecked.Checked = true;
            currenNode.AvailableNeighbors.Push(nodeBeingChecked);

            MinPathLength(ref minPathLength, nodeBeingChecked, minNods);
        }

        return (minNods, minPathLength);
    }

    private void MinPathLength(ref float minPathLength, Node nodeBeingChecked, Stack<Node> minNods)
    {
        if (minPathLength > nodeBeingChecked.EstimatedOfAllPath)
        {
            minPathLength = nodeBeingChecked.EstimatedOfAllPath;
            minNods.Clear();
            minNods.Push(nodeBeingChecked);
        }
        else if (minPathLength == nodeBeingChecked.EstimatedOfAllPath)
        {
            minNods.Push(nodeBeingChecked);
        }
    }

    private bool NodeInGrid(Vector2Int positionBeingChecked)
    {
        return positionBeingChecked.x < 0
               || positionBeingChecked.y < 0
               || positionBeingChecked.x >= Gride.GetLength(0)
               || positionBeingChecked.y >= Gride.GetLength(1);
    }

    private float Heuristics(Vector2Int v)
    {
        return v.magnitude;
    }

    private Stack<Vector2Int> ReconstructPath(Vector2Int start, Stack<Node> currenNodes)
    {
        Stack<Vector2Int> path = new Stack<Vector2Int>();
        
        Node nodeOfPath = currenNodes.Pop();
        while (nodeOfPath.Position != start)
        {
            path.Push(nodeOfPath.Position);
            nodeOfPath = nodeOfPath.Parent;
        }
        path.Push(nodeOfPath.Position);

        return path;
    }
}