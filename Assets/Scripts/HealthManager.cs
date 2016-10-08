using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HealthManager : NetworkBehaviour
{
    [SyncVar]
    public int health = 100;
    private int maxHealth = 100;

    public HP_Visual hpCircle;

    public void Start()
    {
        if (hpCircle == null)
            hpCircle = GetComponentInChildren<HP_Visual>();

        maxHealth = health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        hpCircle.ChangeHealthAmount(health, maxHealth);

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
