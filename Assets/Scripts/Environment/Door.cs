using UnityEngine;

public class Door : MonoBehaviour
{
    private Transform doorTrigger;
    private PlayerInventory playerInventory;

    private void Awake()
    {
        doorTrigger = transform.GetChild(0);
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    private void Update()
    {
        if(doorTrigger.GetComponent<Trigger>().playerIsOnTrigger)
        {
            if(playerInventory.playerHasKey)
            {
                Destroy(gameObject);
            }
        }
    }


}
