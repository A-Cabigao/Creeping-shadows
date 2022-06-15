using UnityEngine;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
    public Grid grid { get; private set; }

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    // A star pathfinding function
    public void FindPath(Vector3 startPosition, Vector3 targetPosition, int index)
    {
        Node startNode = grid.NodeFromWorldPosition(startPosition);
        Node endNode = grid.NodeFromWorldPosition(targetPosition);

        Heap<Node> openList = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closedList = new HashSet<Node>();
         
        openList.AddItemToHeap(startNode);

        while (openList.currentItemCount > 0)
        {
            Node currentNode = openList.RemoveFirstItemInHeap();
            closedList.Add(currentNode);

            if(currentNode == endNode)
            {
                RetracePath(startNode, endNode, index);
                return;
            }

            foreach(Node neighbor in grid.GetNeighborNodes(currentNode))
            {
                if(!neighbor.isWalkable || closedList.Contains(neighbor))
                {
                    continue;
                }

                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if(newMovementCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, endNode);
                    neighbor.parentNode = currentNode;

                    if(!openList.Contains(neighbor))
                    {
                        openList.AddItemToHeap(neighbor);
                    }
                }
            }
        }
    }

    // Retraces the path to the end position made from FindPath back to the reference start position
    private void RetracePath(Node startNode, Node endNode, int index)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }

        path.Reverse();

        grid.generatedPaths.Add(index, path);

        //grid.path = path;
    }


    // Returns total distance from one node to another
    private int GetDistance(Node a, Node b)
    {
        int distanceX = Mathf.Abs(a.gridX - b.gridX);
        int distanceY = Mathf.Abs(a.gridY - b.gridY);

        if(distanceX > distanceY)
        {
            return 14 * distanceY + (10 * distanceX - distanceY);
        }

        return 14 * distanceX + (10 * distanceY - distanceX);
    }
}

// Code credits to Sebastian Lague on Youtube