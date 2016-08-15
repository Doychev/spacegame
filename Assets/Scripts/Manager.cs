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
    private List<NetworkConnection> players = new List<NetworkConnection>();

    void Awake()
    {
        this.matchSize = 2;
    }

    // Use this for initialization
    void Start()
    {
        HUD = GameObject.Find("HUD");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        Debug.Log("OnPlayerConnected");
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("host " + client.connection.connectionId);
        Debug.Log("A client has connected " + conn.connectionId);
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        Debug.Log("a players is ready");
        players.Add(conn);

        if (players.Count >= 2)
        {
            SetupHUD();
        }
    }

    private void SetupHUD()
    {

        var HUD = GameObject.Find("HUD");
        HUD.GetComponent<HUD>().SetupHUD(players);
       
        //var players = HUD.GetComponentsInChildren<Text>();
        //players[0].text = "Player 1:" + this.players[0].connectionId;
        //players[1].text = "Player 2:" + this.players[1].connectionId;
    }
}
