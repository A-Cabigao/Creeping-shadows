using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WendigoMovement : MonoBehaviour
{
    [SerializeField]
    private int indexId;
    [SerializeField]
    private float wendigoSpeed;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private Transform[] patrolSpots;

    private int currentSpotIndex;
    private bool isFindingPath;
    private bool isInPatrol;

    private void Start()
    {
        currentSpotIndex = 0;
        isFindingPath = false;
        isInPatrol = false;
    }

    public bool GetPatrol()
    {
        return isInPatrol;
    }

    public void SetPatrol(bool boo)
    {
        isInPatrol = boo;
    }

    public void SetMogallMovementSpeed(int speed)
    {
        wendigoSpeed = speed;
    }

    // Funtion for default movement pattern of this enemy
    public void PatrolMovement()
    {
        // Check if unit is currently moving from finding path or directly moving to destination
        if (!isFindingPath)
        {
            StartCoroutine(MoveToPath(PathManager.instance.RequestPath(transform.position, patrolSpots[currentSpotIndex].position, indexId)));
        }
    }

    // Function to chase player when player enters field of view of this enemy
    public void ChasePlayerMovement(Transform playerTransform)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerTransform.position,
            Vector3.Distance(transform.position, playerTransform.position), layerMask);

        if (hit)
        {
            if (!isFindingPath)
            {
                StartCoroutine(MoveToPath(PathManager.instance.RequestPath(transform.position, playerTransform.position, indexId)));
            }
        }
        else
        {
            if (!isFindingPath)
            {
                transform.up = (playerTransform.position - transform.position).normalized;
                transform.position += transform.up * wendigoSpeed * Time.deltaTime;
            }
        }
    }

    // Coroutine for patrol movement
    private IEnumerator MoveToPath(List<Node> pathNodes)
    {
        isFindingPath = true;
        List<Vector3> movementSpots = new List<Vector3>();

        // Check if there is a path available to the target position
        if (pathNodes != null)
        {
            foreach (Node node in pathNodes)
            {
                movementSpots.Add(node.worldPosition);
            }
        }
        else
        {
            Debug.LogError("No path found");
            yield break;
        }

        int pathIndex = 0;

        // Loop for movement function that stops if state of enemy is changed
        while (true && isInPatrol)
        {
            // Look towards the destination path
            transform.up = (movementSpots[pathIndex] - transform.position).normalized;

            // Move the enemy towards desitnation path 
            if (Vector3.Distance(transform.position, movementSpots[pathIndex]) > 0.05f)
            {
                if (!GameMaster.gameIsPaused)
                    transform.position += transform.up * wendigoSpeed * Time.deltaTime;

                yield return null;
            }
            else
            {
                pathIndex++;
                if (pathIndex == movementSpots.Count)
                {
                    currentSpotIndex++;

                    if (currentSpotIndex == patrolSpots.Length)
                    {
                        currentSpotIndex = 0;
                    }
                    break;
                }
            }
        }

        // Reset path finding boolean
        // Remove the path taken from pathfinder
        isFindingPath = false;
        PathManager.instance.RemovePathFromList(indexId);
        yield return null;
    }
}
