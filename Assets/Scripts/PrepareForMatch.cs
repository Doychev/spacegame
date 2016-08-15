using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PrepareForMatch : NetworkBehaviour
{

    public GameObject tower;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateDefender()
    {
        CmdSpawn();
    }

    [Command]
    void CmdSpawn()
    {
        var go = (GameObject)Instantiate(tower, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }
}
