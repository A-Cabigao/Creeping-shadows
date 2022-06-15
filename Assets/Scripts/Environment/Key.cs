using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<PlayerInventory>().playerHasKey = true;
        Destroy(gameObject);
    }
}
