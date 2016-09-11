using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AlienBehaviour : NetworkBehaviour
{
    public GameObject moveTarget;
    public GameObject fireTarget;

    [SyncVar(hook = "DecreaseHealthBar")]
    public int health = 100;

    private int maxHealth = 100;

    public int firerate = 10; //1 to 10;
    public int power = 10;//1 to 10;
    public int speed = 10;//1 to 10;

    private Image healthImage;

    public void Start()
    {
        if (moveTarget == null)
            moveTarget = GameObject.Find("HumanBase");

        healthImage = GetComponentInChildren<Image>();
        maxHealth = health;
    }

    public void Update()
    {
        Move();
    }

    protected void Move()
    {
        var target = moveTarget.transform.position;

        if (Vector3.Distance(transform.position, target) > 0.3f)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            var dir = target - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void DecreaseHealthBar(int currentHealthNumber)
    {
        var fill = currentHealthNumber / (float)maxHealth;
        healthImage.fillAmount = fill;
        healthImage.color = Color.Lerp(Color.red, Color.green, fill);
    }
}

