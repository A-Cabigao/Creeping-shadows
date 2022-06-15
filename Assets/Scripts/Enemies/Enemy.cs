using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int maxEnemyHealth;

    private int enemyHealth;

    public Action enemyHasDied;

    private void Start()
    {
        enemyHealth = maxEnemyHealth;   
    }

    public int GetEnemyMaxHealth()
    {
        return maxEnemyHealth;
    }

    public int GetEnemyHealth()
    {
        return enemyHealth;
    }

    public void SetEnemyHealth(int amount)
    {
        enemyHealth = amount;
    }

    public void TakeDamage(int amount)
    {
        enemyHealth -= amount;

        if (enemyHealth < 1)
        {
            enemyHasDied?.Invoke();
        }
    }
}
