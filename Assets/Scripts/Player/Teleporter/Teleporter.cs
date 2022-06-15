using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public float teleporterSpeed = 5f;

    private Rigidbody2D rigidBody;
    private Vector2 travelDirection;
    private Transform cameraTransform;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        cameraTransform = transform.GetChild(0);
    }

    private void Start()
    {
        Destroy(gameObject, 3f);
        transform.up = travelDirection;
        cameraTransform.up = Vector3.zero;
        rigidBody.velocity = travelDirection * teleporterSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    public void SetTravelDirection(Vector2 dir)
    {
        travelDirection = dir;
    }
}
