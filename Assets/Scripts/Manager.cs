using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking.Match;
using System.Collections.Generic;
using UnityEngine.UI;

public class Manager : NetworkManager
{
    private GameObject HUD;
    private GameObject spawner;

    private List<NetworkConnection> players = new List<NetworkConnection>();

    public NetworkConnection defender = null;
    public NetworkConnection attacker = null;

    void Awake()
    {
        this.matchSize = 2;
    }

    // Use this for initialization
    void Start()
    {
        HUD = GameObject.Find("HUD");
        spawner = GameObject.Find("Spawner");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnServerReady(NetworkConnection conn)
    {
        print("A players is ready...");
        players.Add(conn);

        if (players.Count >= 2)
        {
            print("A game begins...");
            NetworkServer.SpawnObjects();
            SetOpposingSides();
            SetupHUD();

            var spawnerScript = GameObject.Find("Spawner").GetComponent<Spawner>();
            spawnerScript.RegisterManager(this);

            spawnerScript.CmdSetTower();
            spawnerScript.RpcFlipCamera();


            //print(NetworkServer.conn);
        }
    }

    //Called on the server when a client disconnects.
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        print("Disconnected connection id " + conn.connectionId);

        players.Clear();
        EmptyHUD();
        StopHost();
    }

    //This hook is called when a host is stopped.
    public override void OnStopHost()
    {
        print("Host has been stopped...");
        EmptyHUD();
        players.Clear();
    }

    // Called on clients when disconnected from a server.
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        print("Disconnected on client...");
        players.Clear();
        EmptyHUD();
    }

    private void SetupHUD()
    {
        var HUD = GameObject.Find("HUD");
        var hudScript = HUD.GetComponent<HUD>();

        var p1Role = defender.connectionId == players[0].connectionId ? "Defender" : "Attacker";
        var p2Role = defender.connectionId == players[1].connectionId ? "Defender" : "Attacker";

        var p1Info = "P1 (Hosting)(" + p1Role + ")";
        var p2Info = "P2 (Joined)(" + p2Role + ")";

        hudScript.p1Info = p1Info;
        hudScript.p2Info = p2Info;
    }

    private void EmptyHUD()
    {
        var HUD = GameObject.Find("HUD");
        var hudScript = HUD.GetComponent<HUD>();
        hudScript.p1Info = "P1";
        hudScript.p2Info = "P2";
    }

    private void SetOpposingSides()
    {
        //This can be random but for now let's keep it non-random for easy testing
        //Change DrawDefender to make it truly random
        var defenderIndex = DrawDefender();
        var attackerIndex = defenderIndex == 0 ? 1 : 0;

        defender = players[defenderIndex];
        attacker = players[attackerIndex];
    }

    private int DrawDefender()
    {
        return 0;
        return Random.Range(0, 2);
    }

}
