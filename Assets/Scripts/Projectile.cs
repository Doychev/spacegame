using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Projectile : NetworkBehaviour
{
    public int damage = 10;
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
        {
            Destroy(gameObject);
            return;
        }

        var targetPosition = this.target.transform.position;

        if (Vector3.Distance(transform.position, targetPosition) > 0.3f)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            var dir = targetPosition - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            this.target.GetComponent<AlienBehaviour>().TakeDamage(damage);
            Destroy(gameObject);
            // Network.Destroy(GetComponent<NetworkView>().viewID);
        }
    }
}
