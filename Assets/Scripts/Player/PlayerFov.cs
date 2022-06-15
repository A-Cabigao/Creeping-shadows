using UnityEngine;

public class PlayerFov : MonoBehaviour
{
    [SerializeField]
    private Transform fovPrefab;

    public FieldOfView fov { get; private set; }
    private Camera mainCam;

    private void Start()
    {
        fov = Instantiate(fovPrefab, null).GetComponent<FieldOfView>();
        mainCam = FindObjectOfType<MainCamera>().mainCam;      
    }

    private void Update()
    {
        if (!GameMaster.gameIsPaused)
        {
            fov.SetOrigin(transform.position);
            transform.up = DirectionToMouse();
        }
    }

    private Vector3 DirectionToMouse()
    {
        Vector3 mouseWorldPosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionToMousePointer = new Vector3(mouseWorldPosition.x - transform.position.x, mouseWorldPosition.y - transform.position.y);
        directionToMousePointer.Normalize();

        return directionToMousePointer;
    }

    private Vector3 MousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = mainCam.ScreenToWorldPoint(mousePosition);
        mousePosition -= transform.position;

        return mousePosition.normalized;
    }
}
