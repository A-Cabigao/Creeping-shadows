using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float playerMovementSpeed;

    private float walkSoundTimer;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        walkSoundTimer = 0f;
    }

    private void Update()
    {
        if (!GameMaster.gameIsPaused)
            MovePlayer();
    }


    //Moves player according to horizontal and vertical input keys.
    private void MovePlayer()
    {
        //animator.Play("Walk");
        walkSoundTimer += Time.deltaTime;
        float horizontal = Input.GetAxis("Horizontal") * playerMovementSpeed;
        float vertical = Input.GetAxis("Vertical") * playerMovementSpeed;     

        transform.position += new Vector3(horizontal, vertical, 0f) * Time.deltaTime;
    }

}
