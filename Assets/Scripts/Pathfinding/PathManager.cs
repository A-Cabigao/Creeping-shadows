using UnityEngine;
using System.Collections.Generic;

public class PathManager : MonoBehaviour
{
    private Pathfinding pathFinding;

    public static PathManager instance;

    private void Awake()
    {
        pathFinding = GetComponent<Pathfinding>();
        
        if(instance != null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
            instance = this;
        }
    }

    public List<Node> RequestPath(Vector3 startPos, Vector3 endPos, int index)
    {
        if(index < 1)
        {         
            Debug.LogError("Index out of bounds");
            return null;
        }
        else
        {
            pathFinding.FindPath(startPos, endPos, index);

            if (pathFinding.grid.generatedPaths[index] != null)
                return pathFinding.grid.generatedPaths[index];

            else
            {
                Debug.LogError("No path found");
                return null;
            }            
        }
    }
    
    public void RemovePathFromList(int index)
    {
        pathFinding.grid.generatedPaths.Remove(index);
    }

}
