using UnityEngine;
using Panda;

public class WendigoController : MonoBehaviour
{
    private Animator animator;
    private EnemyFov fov;
    private Enemy enemy;
    private Wendigo wendigo;
    private WendigoMovement movement;
    private WendigoAttack attack;
    private Player player;
    private Transform playerTransform;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        wendigo = GetComponent<Wendigo>();
        attack = GetComponent<WendigoAttack>();
        enemy = GetComponent<Enemy>();
        movement = GetComponent<WendigoMovement>();

        player = FindObjectOfType<Player>();
        playerTransform = player.transform;
    }

    private void Start()
    {
        fov = GetComponent<EnemyFov>();
    }

    private void Update()
    {
        if(!GameMaster.gameIsPaused)
        {
            Patrol();
            ChasePlayer();
        }
    }

    private void OnEnable()
    {
        enemy.enemyHasDied += DeathState;
        wendigo.rageActivated += Rage;
    }

    private void OnDisable()
    {
        enemy.enemyHasDied -= DeathState;
        wendigo.rageActivated -= Rage;
    }

    private void Patrol()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Patrol"))
        {
            movement.SetPatrol(true);
            movement.PatrolMovement();
        }
    }

    // Function that chases and rotates object towards player position
    private void ChasePlayer()
    {
        bool targetInView = fov.fov.IsTargetInFieldOfView(transform, playerTransform);

        if (playerTransform != null)
        {
            if (targetInView || animator.GetFloat("attentionTime") > 0f)
            {
                movement.SetPatrol(false);
                animator.SetBool("isPlayerSpotted", true);

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Chase"))
                {
                    movement.ChasePlayerMovement(playerTransform);
                }
            }

            animator.SetBool("isPlayerSpotted", targetInView);
            animator.SetFloat("distanceFromPlayer", Vector3.Distance(transform.position, playerTransform.position));
        }
    }

    // Plays the death state in the state machine and exits the state loops
    private void DeathState()
    {
        animator.Play("WendigoDeath");
    }

    private void Rage()
    {
        movement.SetMogallMovementSpeed(5);
        animator.SetFloat("rageAttackInterval", 5f);
    }

    [Task]
    private void CheckIfHealthBelowHalf()
    {
        float max = enemy.GetEnemyMaxHealth();
        float current = enemy.GetEnemyHealth();
        float half =  current / max;

        if (half <= 0.5f)
            Task.current.Complete(true);
        else
            Task.current.Complete(false);
    }
}
