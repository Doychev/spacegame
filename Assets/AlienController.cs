using UnityEngine;
using System.Collections;

public class AlienController : MonoBehaviour {

    private GameObject mainBase;
    public float speed;

	// Use this for initialization
	void Start () {
        mainBase = GameObject.Find("MainBase");
	}
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, mainBase.transform.position, step);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll);
        if (coll.gameObject.name == "MainBase")
        {
            Destroy(gameObject);
        }
    }
}
