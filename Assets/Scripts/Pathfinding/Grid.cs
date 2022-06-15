using UnityEngine;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    private Node[,] nodeGrid;
    private float nodeSize;

    public LayerMask layerMask;
    public float halfNodeSize;
    public Vector2 gridWorldSize;

    public int width, height;
    public bool drawGizmos = true;
    public List<Node> path;

    public Dictionary<int, List<Node>> generatedPaths;

    private void Start()
    {
        nodeSize = halfNodeSize * 2f;
        width = Mathf.RoundToInt(gridWorldSize.x / nodeSize);
        height = Mathf.RoundToInt(gridWorldSize.y / nodeSize);

        generatedPaths = new Dictionary<int, List<Node>>();
        CreateGrid();
    }

    public int MaxSize
    {
        get { return width * height; }
    }

    // Returns a node from a specified world position
    public Node NodeFromWorldPosition(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2f) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2f) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((width - 1) * percentX);
        int y = Mathf.RoundToInt((height - 1) * percentY);
        return nodeGrid[x, y];
    }

    // Checks all neighbor nodes from the current node starting from bottom left neighbor
    public List<Node> GetNeighborNodes(Node node)
    {
        List<Node> neighbors = new List<Node>();
        for(int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < height)
                    neighbors.Add(nodeGrid[checkX, checkY]);
            }
        }

        return neighbors;
    }

    // Generates the grid for pathfinding starting from the bottom left world position 
    private void CreateGrid()
    {
        nodeGrid = new Node[width, height];
        Vector3 worldBottomLeftPosition = transform.position - Vector3.right * gridWorldSize.x / 2f - Vector3.up * gridWorldSize.y / 2f;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 worldPosition = worldBottomLeftPosition + Vector3.right *
                    (i * nodeSize + halfNodeSize) + Vector3.up * (j * nodeSize + halfNodeSize);
                bool walkable = !Physics2D.OverlapCircle(worldPosition, halfNodeSize, layerMask);
                nodeGrid[i, j] = new Node(walkable, worldPosition, i, j);


            }
        }
    }

    //DEBUG ONLY
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1f));

    //    if (nodeGrid != null)
    //    {
    //        foreach (Node node in nodeGrid)
    //        {
    //            Gizmos.color = (node.isWalkable) ? Color.white : Color.red;

    //            if (generatedPaths[1] != null)
    //            {
    //                if (generatedPaths[1].Contains(node))
    //                {
    //                    Gizmos.color = Color.black;
    //                }
    //            }

    //            Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeSize - 0.1f));
    //        }
    //    }
    //}
}

// Code credits to Sebastian Lague on Youtube
