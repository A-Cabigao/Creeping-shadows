using UnityEngine;
using System;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private int shieldCrashDamage;
    public Action CollidedWithPlayer;
    public bool hasCollidedWithPlayer { get; private set; }

    private void Start()
    {
        hasCollidedWithPlayer = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            if(!hasCollidedWithPlayer)
            {
                collision.transform.GetComponent<Player>().TakeDamage(shieldCrashDamage);

                hasCollidedWithPlayer = true;
                CollidedWithPlayer?.Invoke();
            }
        }
    }

    public void ResetCollisionBool()
    {
        hasCollidedWithPlayer = false;
    }
}
