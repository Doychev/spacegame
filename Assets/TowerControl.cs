using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TowerControl : NetworkBehaviour
{

    private SpriteRenderer selectedIndicator;

    // Use this for initialization
    void Start()
    {

        selectedIndicator = transform.GetChild(0).GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        if (localPlayerAuthority && Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit && hit.transform.tag == "Tower")
                SelectTower();
        }

    }

    void SelectTower()
    {
        selectedIndicator.enabled = true;
    }
}
