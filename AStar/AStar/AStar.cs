using System;
using System.Collections.Generic;

public class AStar
{
    private char[,] maze;

    // 	OPEN = priority queue containing START
    private PriorityQueue<Node> openQueue;
    // dictionary storing the node from which we have reached a node (following a path)
    private Dictionary<Node, Node> parents;
    // dictionary storing cost from the start to a node (following a path)
    private Dictionary<Node, int> gCost;

    public AStar(char[,] map)
    {
        this.maze = map;
        this.openQueue = new PriorityQueue<Node>();
        this.parents = new Dictionary<Node, Node>();
        this.gCost = new Dictionary<Node, int>();
    }

    public static int GetH(Node current, Node goal)
    {
        var deltaX = Math.Abs(current.Col - goal.Col);
        var deltaY = Math.Abs(current.Row - goal.Row);

        return deltaX + deltaY;
    }

    public IEnumerable<Node> GetPath(Node start, Node goal)
    {
        parents[start] = null;
        gCost[start] = 0;
        openQueue.Enqueue(start);

        while (openQueue.Count > 0)
        {
            Node current = openQueue.Dequeue();
            if (current.Equals(goal))
            {
                break;
            }
            List<Node> neighbors = GetNeighbors(current);
            foreach (Node neighbor in neighbors)
            {
                var newCost = gCost[current] + 1;
                if (!gCost.ContainsKey(neighbor) || newCost < gCost[neighbor])
                {
                    gCost[neighbor] = newCost;
                    neighbor.F = newCost + GetH(neighbor, goal);
                    openQueue.Enqueue(neighbor);
                    parents[neighbor] = current;
                }
            }
        }

        return this.ReconstructPath(parents, start, goal);
    }

    private IEnumerable<Node> ReconstructPath(Dictionary<Node, Node> parents, Node start, Node goal)
    {
        if (!parents.ContainsKey(goal))
        {
            return new List<Node>()
            {
               start
            };
        }
        Node current = parents[goal];
        Stack<Node> path = new Stack<Node>();
        path.Push(goal);

        while (!current.Equals(start))
        {
            path.Push(current);
            current = parents[current];
        }

        path.Push(start);
        return path;
    }

    private List<Node> GetNeighbors(Node current)
    {
        int rowUp = current.Row + 1;
        int colRight = current.Col + 1;
        int rowDown = current.Row - 1;
        int colLeft = current.Col - 1;

        List<Node> result = new List<Node>();
        this.AddToNeighbors(result, rowUp, current.Col);
        this.AddToNeighbors(result, current.Row, colRight);
        this.AddToNeighbors(result, rowDown, current.Col);
        this.AddToNeighbors(result, current.Row, colLeft);
        return result;
    }

    private void AddToNeighbors(List<Node> list, int row, int col)
    {
        if (IsInBounds(row, col) && !IsWall(row, col))
        {
            Node newNode = new Node(row, col);
            list.Add(newNode);
        }
    }

    private bool IsInBounds(int row, int col)
    {
        return (row >= 0 && row < this.maze.GetLength(0))
            && (col >= 0 && col < this.maze.GetLength(1));
    }

    private bool IsWall(int row, int col)
    {
        return this.maze[row, col] == 'W';
    }
}

