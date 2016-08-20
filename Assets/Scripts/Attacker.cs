using UnityEngine;
using System.Collections;

using UnityEngine.Networking;

public class Attacker : PlayerController
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FlipCamera()
    {
        Camera.main.transform.Rotate(new Vector3(0, 0, 180));
    }

    //
    //  [Command]
    //  public void CmdPrepareDefender()
    //  {
    //      var go = (GameObject)Instantiate(manager.spawnPrefabs[0], transform.position + new Vector3(2, 2, 0), Quaternion.identity);
    //
    //      go.GetComponent<NetworkIdentity>().localPlayerAuthority = true;
    //      NetworkServer.Spawn(go);
    //  }
}
