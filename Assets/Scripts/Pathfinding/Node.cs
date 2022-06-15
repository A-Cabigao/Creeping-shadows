using UnityEngine;

public class Node : IHeapItem<Node>
{
    public bool isWalkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parentNode;

    private int heapIndex;

    public Node(bool walkable, Vector3 worldPos, int x, int y)
    {
        isWalkable = walkable;
        worldPosition = worldPos;
        gridX = x;
        gridY = y;
    }

    public int fCost
    {
        get { return gCost + hCost;}
    }

    public int HeapIndex
    { 
        get { return heapIndex; }
        set { heapIndex = value; }
    }

    public int CompareTo(Node compareNode)
    {
        int compare = fCost.CompareTo(compareNode.fCost);
        if(compare == 0)
        {
            compare = hCost.CompareTo(compareNode.hCost);
        }
        return -compare;
    }
}

// Code credits to Sebastian Lague on Youtube
