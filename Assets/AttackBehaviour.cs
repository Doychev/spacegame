using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class AttackBehaviour : NetworkBehaviour
{
    public float fireCooldown = 1; //1 to 10;
    public float? range = null;
    private float currentCooldown;

    public GameObject target;
    public GameObject projectile;

    // Use this for initialization
    void Start()
    {
        //      targetListBacklog.
        if (range == null)
            throw new Exception("Attack range has to be set from outside");

        currentCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCooldown > 0)
            currentCooldown -= Time.deltaTime;

        //Fire at stuff if necessary 
        if (target != null)
        {
            var distance = Vector2.Distance(target.transform.position, transform.position);

            if (distance < range)
            {
                //Debug.DrawLine(transform.position, target.transform.position);
                Fire();
            }
        }
    }

    public void Fire()
    {

        //target = this.target;

        if (currentCooldown <= 0)
        {
            CmdCreateProjectile();
            currentCooldown = fireCooldown;
        }

    }

    [Command]
    void CmdCreateProjectile()
    {
        var proj = (GameObject)Instantiate(projectile, transform.position, new Quaternion());
        proj.GetComponent<Projectile>().target = this.target;
        NetworkServer.Spawn(proj);
    }
}
