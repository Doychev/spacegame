using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TowerControl : NetworkBehaviour
{
    public int health = 100;
    public int firerate = 10; //1 to 10;
    public int power = 10;//1 to 10;
    public int speed = 5;//1 to 10;
    public float range = 2;

    public GameObject target;

    private int cooldown;

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

        if (hasAuthority && Input.GetMouseButtonDown(0))
        {
            var mouseTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseTarget, Vector2.zero);

            if (hit && hit.transform.tag == "Tower")
            {
                selectedIndicatorSprite.enabled = !selectedIndicatorSprite.enabled;
                isSelected = !isSelected;
            }
            else if (isSelected)
            {
                SetWayPoint(mouseTarget);
            }

        }

        var distance = Vector2.Distance(target.transform.position, transform.position);

        if (target != null && distance < range)
        {
            Debug.DrawLine(transform.position, target.transform.position);
            Fire();
        }
        else
            GetTarget();

        Move();
    }

    void SelectTower()
    {
        isSelected = true;
        selectedIndicatorSprite.enabled = true;
    }

    void DeselectTower()
    {
        isSelected = false;
        selectedIndicatorSprite.enabled = false;
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
    }

    void ResizeRangeVizualization(GameObject selectedIndicatorObject)
    {
        selectedIndicatorObject.transform.localScale = new Vector3(range, range, range);
    }

    GameObject GetTarget()
    {
        var aliensLayer = LayerMask.NameToLayer("Aliens");
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 50, transform.right, 500, aliensLayer);

        if (hit)
        {
            print(hit.transform.name);
        }

        return null;
    }

}
