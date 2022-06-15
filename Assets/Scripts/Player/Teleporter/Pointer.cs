using UnityEngine;

public class Pointer : MonoBehaviour
{
    public float boundingRadiusToPlayer = 1.5f;

    public Vector2 travelDirection { get; private set; }

    private Transform player;
    private Camera cam;

    private void Start()
    {
        cam = FindObjectOfType<MainCamera>().mainCam;
        player = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        if (!GameMaster.gameIsPaused)
            UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = cam.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - player.position.x, mousePosition.y - player.position.y);
        direction.Normalize();
        transform.up = direction;

        travelDirection = direction;

        transform.position = player.position + new Vector3(direction.x * boundingRadiusToPlayer, direction.y * boundingRadiusToPlayer, 0f).normalized;
    }

    
}
