using UnityEngine;
using System.Collections;

public class SelectTarget : MonoBehaviour
{
    AttackBehaviour tower;

    // Use this for initialization
    void Start()
    {
        tower = transform.parent.GetComponent<AttackBehaviour>();
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
