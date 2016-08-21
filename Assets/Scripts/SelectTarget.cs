using UnityEngine;
using System.Collections;

public class SelectTarget : MonoBehaviour
{
    TowerControl tower;

    // Use this for initialization
    void Start()
    {
        tower = transform.parent.GetComponent<TowerControl>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Aliens")
        {
            tower.target = other.gameObject;
        }
    }
}
