using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TowerControl : NetworkBehaviour
{
    public int health = 100;
    public int firerate = 10; //1 to 10;
    public int power = 10;//1 to 10;
    public int speed = 10;//1 to 10;

    private SpriteRenderer selectedIndicator;
    private bool isSelected = false;

    [SyncVar]
    public Vector3 waypoint;

    public GameObject manger;

    // Use this for initialization
    void Start()
    {

        selectedIndicator = transform.GetChild(0).GetComponent<SpriteRenderer>();

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
                selectedIndicator.enabled = !selectedIndicator.enabled;
                isSelected = !isSelected;
            }
            else if (isSelected)
            {
                SetWayPoint(mouseTarget);
            }

        }

        Move();
    }

    void SelectTower()
    {
        isSelected = true;
        selectedIndicator.enabled = true;
    }

    void DeselectTower()
    {
        isSelected = false;
        selectedIndicator.enabled = false;
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
}
