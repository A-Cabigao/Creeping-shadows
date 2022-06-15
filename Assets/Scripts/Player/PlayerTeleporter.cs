using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField]
    private GameObject teleporterPointerPrefab;
    [SerializeField]
    private GameObject teleporterPrefab;

    private GameObject teleporterPointerObject;
    private GameObject teleporterObject;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!GameMaster.gameIsPaused)
        {
            if (teleporterPointerObject == null)
            {
                if (Input.GetKeyDown(KeyCode.Space) && teleporterObject == null)
                {
                    animator.Play("BowDraw");
                    InstantiateTeleporterPointer();
                    AudioManager.instance.PlayPlayerAudio("Bow_Draw", transform);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    animator.Play("Idle");
                    Destroy(teleporterPointerObject);
                }
            }

            if(teleporterPointerObject != null && teleporterObject == null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    animator.Play("BowFire");
                    AudioManager.instance.PlayPlayerAudio("Bow_Fire", transform);
                    InstantiateTeleporter();
                    Destroy(teleporterPointerObject);
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0) && teleporterObject != null)
                {
                    AudioManager.instance.PlayPlayerAudio("Teleport", transform);
                    transform.position = teleporterObject.transform.position;
                    Destroy(teleporterObject);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    animator.Play("Idle");
                    Destroy(teleporterObject);
                }
            }
        }            
    }

    // Instantiates the pointer to point the direction of where to fire teleporter
    private void InstantiateTeleporterPointer()
    {
        teleporterPointerObject = Instantiate(teleporterPointerPrefab);
    }

    // Instantiates teleporter 
    private void InstantiateTeleporter()
    {
        teleporterObject = Instantiate(teleporterPrefab, teleporterPointerObject.transform.position,Quaternion.identity);
        teleporterObject.GetComponent<Teleporter>().SetTravelDirection(teleporterPointerObject.GetComponent<Pointer>().travelDirection);
    }
}
