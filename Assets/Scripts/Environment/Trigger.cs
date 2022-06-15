using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool playerIsOnTrigger { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            playerIsOnTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            playerIsOnTrigger = false;
    }
}
