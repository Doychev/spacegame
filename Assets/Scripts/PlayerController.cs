using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

    public GameObject alienPrefab;

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 2.0f;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * 2.0f;

        transform.Translate(x, y, 0);

        if (Input.GetMouseButtonDown(0))
        {
            CmdSpawn(Camera.main.ScreenToWorldPoint(Input.mousePosition), gameObject);
        }
    }

    [Command]
    void CmdSpawn(Vector3 mousePosition, GameObject player)
    {
        mousePosition.z = 0f;
        var alien = (GameObject)Instantiate(
             alienPrefab,
             mousePosition,
             Quaternion.identity);

        NetworkServer.SpawnWithClientAuthority(alien, player);
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}