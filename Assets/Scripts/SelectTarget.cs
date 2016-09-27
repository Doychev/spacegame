using UnityEngine;
using System.Collections;

public class SelectTarget : MonoBehaviour
{
    AttackBehaviour attackBehaviour;

    // Use this for initialization
    void Start()
    {
        attackBehaviour = transform.parent.GetComponent<AttackBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Aliens")
        {
            attackBehaviour.target = other.gameObject;
        }
    }
}
