using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Transform player;

    public Camera mainCam { get; private set; }

    private void Awake()
    {
        mainCam = GetComponent<Camera>();
        player = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
