using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class Defender : NetworkBehaviour
{
    private LobbyManager manager;

    // Use this for initialization
    void Start()
    {
        var lobbyManager = GameObject.Find("LobbyManager");
        manager = lobbyManager.GetComponent<LobbyManager>();

        FlipDefenderCamera();
        CmdPrepareDefender();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FlipDefenderCamera()
    {
        if (isLocalPlayer)
            Camera.main.transform.Rotate(new Vector3(0, 0, 180));
    }

    [Command]
    public void CmdPrepareDefender()
    {
        var go = (GameObject)Instantiate(manager.spawnPrefabs[0], transform.position + new Vector3(3, 3, 0), Quaternion.identity);

        go.GetComponent<NetworkIdentity>().localPlayerAuthority = true;
        NetworkServer.Spawn(go);
    }

}
