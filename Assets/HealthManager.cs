using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour
{

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
