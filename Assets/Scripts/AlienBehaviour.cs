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
    public int firerate = 10; //1 to 10;
    public int range = 10;
    public int power = 10;//1 to 10;
    public int speed = 10;//1 to 10;

    private HP_Visual hpCircle;
    private AttackBehaviour attackBehaviour;

    public void Start()
    {
        if (moveTarget == null)
            moveTarget = GameObject.Find("HumanBaseTarget");

        if (fireTarget == null)
            fireTarget = GameObject.Find("HumanBase");

        attackBehaviour = GetComponent<AttackBehaviour>();
        hpCircle = GetComponentInChildren<HP_Visual>();
        attackBehaviour.range = range;
        attackBehaviour.target = fireTarget;
    }

    public void Update()
    {
        //Stupid hack for OnTriggerStay2D not working right in AttackBehaviour...
        transform.position += new Vector3(0.00001f, 0);

        if (Vector3.Distance(transform.position, moveTarget.transform.position) > 0.3f)
            Move();
        else {
            attackBehaviour.Fire();
        }

    }

    protected void Move()
    {
        var target = moveTarget.transform.position;

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        var dir = target - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}

