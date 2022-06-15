using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private int attackDamage;
    [SerializeField]
    private float attackInterval;
    [SerializeField]
    private LayerMask attackTargetLayer;

    private float attackCooldown = 0f;
    private bool isAttackOnCooldown = false;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!GameMaster.gameIsPaused)
        {
            if(!isAttackOnCooldown)
            {
                if(Input.GetMouseButtonDown(0))
                    Attack();
            }
            else
            {
                attackCooldown += Time.deltaTime;
                if (attackCooldown > attackInterval)
                {
                    isAttackOnCooldown = false;
                    attackCooldown = 0f;
                }
            }

        }
    }


    // Function that checks if an enemy object is in front and deals damage
    private void Attack()
    {
        animator.Play("Attack");
        isAttackOnCooldown = true;

        AudioManager.instance.PlayPlayerAudio("Dagger", transform);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 3f, attackTargetLayer);

        if(hit)
        {
            hit.transform.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
        
    }

}
