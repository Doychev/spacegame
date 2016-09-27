using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TowerControl : NetworkBehaviour
{
    public int health = 100;
    
    public int power = 10;//1 to 10;
    public int speed = 5;//1 to 10;
    public float range = 2;

    public GameObject target;

    private SpriteRenderer selectedIndicatorSprite;

    private bool isSelected = false;

    [SyncVar]
    public Vector3 waypoint;

    public GameObject manger;

    private AttackBehaviour attackBehaviour;

    // Use this for initialization
    void Start()
    {
        var selectedIndicator = transform.GetChild(0).gameObject;
        selectedIndicatorSprite = selectedIndicator.GetComponent<SpriteRenderer>();
        ResizeRangeVizualization(selectedIndicator);
        attackBehaviour = GetComponent<AttackBehaviour>();
        attackBehaviour.range = this.range;
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
    
    void ResizeRangeVizualization(GameObject selectedIndicatorObject)
    {
        selectedIndicatorObject.transform.localScale = new Vector3(range, range, range);
    }

}
