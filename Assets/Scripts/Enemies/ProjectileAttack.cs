using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    [SerializeField]
    private int attackDamage;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    public int GetAttackDamage()
    {
        return attackDamage;
    }

    public void InitializeAttack(float x, float y, float speed, int damage)
    {
        attackDamage = damage;
        rigidBody.velocity = new Vector2(x, y) * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            collision.transform.GetComponent<Player>().TakeDamage(attackDamage);


        Destroy(gameObject);
    }
}
