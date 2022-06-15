using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int playerMaxHealth;

    private int playerHealth;
    public Action PlayerHasDied;
    public Action PlayerTookDamage;

    private void Start()
    {
        playerHealth = playerMaxHealth;
    }

    // Return player's CURRENT health
    public int GetPlayerHealth()
    {
        return playerHealth;
    }

    // Return player's max health
    public int GetPlayerMaxHealth()
    {
        return playerMaxHealth;
    }

    public void SetPlayerHealth(int amount)
    {
        playerHealth = amount;
    }

    // Reduces player health and ends game when health drops to 0
    public void TakeDamage(int amount)
    {
        playerHealth -= amount;
        PlayerTookDamage?.Invoke();

        if (playerHealth < 1)
            PlayerHasDied?.Invoke();
    }
}
