using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TowerControl : NetworkBehaviour
{
    public int health = 100;
    public float fireCooldown = 1; //1 to 10;
    public int power = 10;//1 to 10;
    public int speed = 5;//1 to 10;
    public float range = 2;

    public GameObject projectile;

    public GameObject target;

    private float cooldown;

    private SpriteRenderer selectedIndicatorSprite;

    private bool isSelected = false;

    [SyncVar]
    public Vector3 waypoint;

    public GameObject manger;

    // Use this for initialization
    void Start()
    {
        cooldown = 0;

        var selectedIndicator = transform.GetChild(0).gameObject;
        selectedIndicatorSprite = selectedIndicator.GetComponent<SpriteRenderer>();
        ResizeRangeVizualization(selectedIndicator);
    }

    // Update is called once per frame
    void Update()
    {
        //Select and set waypoint for tower and move the tower if necessary
        if (hasAuthority && Input.GetMouseButtonDown(0))
        {
            var mouseTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseTarget, Vector2.zero, 500, 1 << 9);

            if (hit && hit.transform.tag == "Tower")
            {
                print("are we her??");
                selectedIndicatorSprite.enabled = !selectedIndicatorSprite.enabled;
                isSelected = !isSelected;
            }
            else if (isSelected)
            {
                SetWayPoint(mouseTarget);
            }
        }

        Move();

        //Fire at stuff if necessary 
        if (target != null)
        {
            var distance = Vector2.Distance(target.transform.position, transform.position);

            if (distance < range)
            {
                Debug.DrawLine(transform.position, target.transform.position);
                Fire();
            }
        }

        if (cooldown > 0)
            cooldown -= Time.deltaTime;
    }

    void SetWayPoint(Vector3 target)
    {
        waypoint = new Vector3(target.x, target.y, 0);
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, waypoint) > 0.3f)
        {

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, waypoint, step);
        }
    }

    void Fire()
    {

        print("Pew pew pew...");

        if (cooldown <= 0 && target != null)
        {
            CmdCreateProjectile();
            cooldown = fireCooldown;
        }

    }

    void ResizeRangeVizualization(GameObject selectedIndicatorObject)
    {
        selectedIndicatorObject.transform.localScale = new Vector3(range, range, range);
    }

    [Command]
    void CmdCreateProjectile()
    {
        var proj = (GameObject)Instantiate(projectile, transform.position, new Quaternion());
        proj.GetComponent<Projectile>().target = this.target;
        NetworkServer.Spawn(proj);
    }
}
