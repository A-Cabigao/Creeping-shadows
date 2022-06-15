using UnityEngine;

public class MogallController : MonoBehaviour
{
    private Animator animator;
    private EnemyFov fov;
    private Enemy enemy;
    private Mogall mogall;
    private MogallMovement movement;
    private MogalAttack attack;
    private Player player;
    private Transform playerTransform;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        mogall = GetComponent<Mogall>();
        enemy = GetComponent<Enemy>();
        movement = GetComponent<MogallMovement>();
        attack = GetComponent<MogalAttack>();

        player = FindObjectOfType<Player>();
        playerTransform = player.transform;
    }

    private void Start()
    {
        fov = GetComponent<EnemyFov>();
        InvokeRepeating("SetPlayerSpotted", 1f, 0.15f);
    }

    private void OnEnable()
    {
        enemy.enemyHasDied += DeathState;
    }

    private void OnDisable()
    {
        enemy.enemyHasDied -= DeathState;
    }

    private void Update()
    {
        if(!GameMaster.gameIsPaused)
        {
            PatrolState();
            AttackState();          
        }
    }

    private void PatrolState()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Patrol"))
        {
            movement.Patrol();
        }
    }

    private void AttackState()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            attack.AttackPlayer(playerTransform, animator.GetFloat("attackInterval"));
        }
    }

    private void SetPlayerSpotted()
    {
        bool playerSpotted = fov.fov.IsTargetInFieldOfView(transform, playerTransform);

        animator.SetBool("isPlayerSpotted", playerSpotted);
    }

    // Plays the death state in the state machine and exits the state loops
    private void DeathState()
    {
        animator.Play("MogallDeath");
    }
}
