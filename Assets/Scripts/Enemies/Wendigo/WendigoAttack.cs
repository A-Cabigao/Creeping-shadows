using UnityEngine;

public class WendigoAttack : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private int wendigoDamage;

    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void Attack()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 3f, layerMask);
        if(hit)
        {
            if (hit.collider.tag.Equals("Player"))
                player.TakeDamage(wendigoDamage);
        }
    }
}
