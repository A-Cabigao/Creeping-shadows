using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
    [SerializeField]
    private PathManager pathManager;
    [SerializeField]
    private Transform targetPosition;

    private List<Vector3> pathToTarget;
    private int indexId;

    private void Awake()
    {
        pathToTarget = new List<Vector3>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(TravelToPath());
        }
    }

    private IEnumerator TravelToPath()
    {
        List<Node> travelPath = pathManager.RequestPath(transform.position, targetPosition.position, 1);

        if (travelPath != null)
        {
            foreach (Node node in travelPath)
            {
                pathToTarget.Add(node.worldPosition);
            }
        }
        else
        {
            yield break;
        }

        int travelIndex = 0;

        while(true)
        {
            transform.right = (pathToTarget[travelIndex] - transform.position).normalized;

            if (Vector3.Distance(transform.position, pathToTarget[travelIndex]) > 0.15f)
            {            
                transform.position += transform.right * 5f * Time.deltaTime;
            }
            else
            {
                travelIndex++;

                if (travelIndex == pathToTarget.Count)
                {
                    pathManager.RemovePathFromList(1);
                    yield break;
                }
            }
            yield return null;
        }
    }
}
