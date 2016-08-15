using UnityEngine;
using System.Collections;
using Prototype.NetworkLobby;
using UnityEngine.Networking;

public class Attacker : NetworkBehaviour
{

    private LobbyManager manager;

    // Use this for initialization
    void Start()
    {
        var lobbyManager = GameObject.Find("LobbyManager");
        manager = lobbyManager.GetComponent<LobbyManager>();

        // FlipDefenderCamera();
        CmdPrepareDefender();
        print("connected");
    }

    void OnPlayerConnected(NetworkPlayer player)
    {

        print("connected");
        var lobbyManager = GameObject.Find("LobbyManager");
        manager = lobbyManager.GetComponent<LobbyManager>();
        CmdPrepareDefender();
    }

    // Update is called once per frame
    void Update()
    {
        CmdWrite();
    }

    [Command]
    public void CmdWrite()
    {
        print("start walking...");
    }

    private void FlipDefenderCamera()
    {
        if (isLocalPlayer)
            Camera.main.transform.Rotate(new Vector3(0, 0, 180));
    }

    [Command]
    public void CmdPrepareDefender()
    {
        var go = (GameObject)Instantiate(manager.spawnPrefabs[0], transform.position + new Vector3(2, 2, 0), Quaternion.identity);

        go.GetComponent<NetworkIdentity>().localPlayerAuthority = true;
        NetworkServer.Spawn(go);
    }
}
