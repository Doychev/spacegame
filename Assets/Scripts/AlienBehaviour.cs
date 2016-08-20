using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AlienBehaviour : MonoBehaviour
{

    public GameObject moveTarget;
    public GameObject fireTarget;

    public int health = 100;
    public int firerate = 10; //1 to 10;
    public int power = 10;//1 to 10;
    public int speed = 10;//1 to 10;

    public void Start()
    {
        if (moveTarget == null)
            moveTarget = GameObject.Find("HumanBase");
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

}

