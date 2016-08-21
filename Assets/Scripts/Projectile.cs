using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Projectile : NetworkBehaviour
{

    public int speed = 10;

    [SyncVar]
    public GameObject target;

    public void Start()
    {
        if (target == null)
            print("A projectile was spawned without a target...");
    }

    public void Update()
    {
        Move();
    }

    protected void Move()
    {
        if (this.target == null)
            return;

        var target = this.target.transform.position;

        if (Vector3.Distance(transform.position, target) > 0.3f)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            var dir = target - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            Destroy(gameObject);
            // Network.Destroy(GetComponent<NetworkView>().viewID);
        }
    }
}
