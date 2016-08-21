using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Spawner : NetworkBehaviour
{
    private Manager manager;
    public GameObject tower;
    public GameObject attackerControl;

    public void RegisterManager(Manager manager)
    {
        this.manager = manager;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [Command]
    public void CmdSetTower()
    {
        var t = (GameObject)Instantiate(tower);
        NetworkServer.SpawnWithClientAuthority(t, manager.defender);
    }

    [ClientRpc]
    public void RpcFlipCamera()
    {
        var f = (GameObject)Instantiate(attackerControl);
        NetworkServer.SpawnWithClientAuthority(f, manager.attacker);
    }
}

