using UnityEngine;
using System.Collections;

public class SpinBase : MonoBehaviour
{
    public float rotationSpeed = 1.25f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around its local Y axis at 1 degree per second
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
