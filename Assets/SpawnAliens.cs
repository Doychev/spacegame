using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpawnAliens : NetworkBehaviour
{
    public GameObject alien0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * 2.0f;
            var y = Input.GetAxis("Vertical") * Time.deltaTime * 2.0f;

            transform.Translate(x, y, 0);

            CmdSpawn(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    [Command]
    void CmdSpawn(Vector3 mousePosition)
    {
        mousePosition.z = 0f;
        var alien = (GameObject)Instantiate(
             alien0,
             mousePosition,
             Quaternion.identity);

        NetworkServer.Spawn(alien);
    }


}
