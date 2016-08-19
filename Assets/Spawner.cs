using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Spawner : NetworkBehaviour
{
    //  private NetworkConnection attacker;
    //  private NetworkConnection defender;
    private Manager manager;
    public GameObject tower;

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
}
