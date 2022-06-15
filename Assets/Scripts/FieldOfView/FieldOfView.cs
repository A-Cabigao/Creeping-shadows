using UnityEngine;
using System;

public class FieldOfView : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    private Mesh mesh;
    private Vector3 origin;
    private float startingAngle;

    [Range(0,360)]
    public float fov;
    public float viewDistance;
    public int rayCount = 10;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
    }

    private void Update()
    {
        mesh.RecalculateBounds();
    }

    private void LateUpdate()
    {      
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2d = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);

            if (raycastHit2d.collider == null)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
                RepositionVertex(ref vertex, raycastHit2d);
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;

        }

        // Triangle vector mesh set-up

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    // Repositions vertex to completely render obstacle up to its bounds
    private void RepositionVertex(ref Vector3 vertex, RaycastHit2D hitInfo)
    {
        Vector3 point = hitInfo.point;
        Vector2 maxBound = hitInfo.collider.bounds.max;
        Vector2 minBound = hitInfo.collider.bounds.min;

        // Closer to max bound y
        if ((maxBound.y - point.y) < (point.y - minBound.y))
        {
            if (vertex.y < minBound.y)
                vertex.y = minBound.y;
        }
        // Closer to min bound y
        else
        {
            if (vertex.y > maxBound.y)
                vertex.y = maxBound.y;
        }

        // Closer to max bound x
        if ((maxBound.x - point.x) < (point.x - minBound.x))
        {
            if (vertex.x < minBound.x)
                vertex.x = minBound.x;


        }
        // Closer to min bound x
        else
        {
            if (vertex.x > maxBound.x)
                vertex.x = maxBound.x;
        }
    }

    // returns true if second object is inside first object's field of view
    public bool IsTargetInFieldOfView(Transform firstObjectPosition, Transform secondObjectPosition)
    {
        bool objectDetected = false;

        if(Vector3.Distance(firstObjectPosition.position, secondObjectPosition.position) < viewDistance)
        {
            Vector3 direction = (secondObjectPosition.position - firstObjectPosition.position).normalized;
            if (Vector3.Angle(GetVectorFromAngle(startingAngle), direction) < fov)
            {
                RaycastHit2D hit = Physics2D.Raycast(firstObjectPosition.position + (firstObjectPosition.up * firstObjectPosition.localScale.x), 
                    direction, viewDistance);

                if (hit)
                {
                    if (hit.collider.transform == secondObjectPosition)
                    {
                        objectDetected = true;
                    }
                }
            }
        }
        return objectDetected;
    }

    // Convert float angle into a vector3
    public Vector3 GetVectorFromAngle(float angle)
    {
        // angle = 0 -> 360
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    // Convert Vector3 into a float angle
    public float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (n < 0) 
            n += 360;

        return n;
    }

    // Sets origin where field of view mesh will be drawn
    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    // Sets direction where fov will be drawn
    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVectorFloat(aimDirection) + fov / 2f;
    }
   
}

// Code credits to youtuber CodeMonkey