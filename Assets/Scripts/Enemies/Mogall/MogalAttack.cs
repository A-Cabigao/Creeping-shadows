using UnityEngine;

public class MogalAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject attackPrefab;
    [SerializeField]
    private float mogallAttackSpeed;

    private Transform attackSpawn;

    private void Awake()
    {
        attackSpawn = transform.GetChild(0);
    }

    // Function that rotates enemy to player position, and releases an attack between attack intervals
    public void AttackPlayer(Transform playerTransform, float attackInterval)
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        directionToPlayer.Normalize();

        transform.up = directionToPlayer;

        if(attackInterval < 0f)
        {
            GameObject obj = Instantiate(attackPrefab, attackSpawn.position, Quaternion.identity);
            ProjectileAttack attack = obj.GetComponent<ProjectileAttack>();

            attack.InitializeAttack(directionToPlayer.x, directionToPlayer.y, mogallAttackSpeed, attack.GetAttackDamage());     
        }
    }
}
