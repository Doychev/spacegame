using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using Prototype.NetworkLobby;
using UnityEngine.UI;

public class GameManagement : NetworkBehaviour
{
    public LobbyPlayer[] playerList;

    public GameObject playersUI;

    private int defender, attacker;
    private NetworkManager manager;

    private GameObject playerObject;

    void Start()
    {
        var lobbyManager = GameObject.Find("LobbyManager");
        var manager = lobbyManager.GetComponent<LobbyManager>();
        playerObject = manager.client.connection.playerControllers[0].gameObject;

        RegisterManager(manager);

        RegisterPlayers(lobbyManager.GetComponentsInChildren<LobbyPlayer>(true));

        defender = DrawDefender();
        attacker = (defender + 1) % 2;

       // Invoke(RpcSetupPlayerDashboard, 50);
        //  Invoke(RpcSetupPlayerDashboard(), 3);
        //  RpcSetupPlayerDashboard();

        if (isCurrentClientSelectedAsDefender())
        {
            playerObject.GetComponent<Defender>().enabled = true;
        }
        else if (isCurrentClientSelectedAsAttacker())
        {
            playerObject.GetComponent<Attacker>().enabled = true;
        }
    }

    public void RegisterPlayers(LobbyPlayer[] players)
    {
        this.playerList = players;
    }

    public void RegisterManager(NetworkManager m)
    {
        manager = m;
    }

    private int DrawDefender()
    {
        return Random.Range(0, 1);
    }

    public void Update()
    {

    }

    [ClientRpc]
    private void RpcSetupPlayerDashboard()
    {
        var p1Text = playersUI.transform.Find("P1").GetComponent<Text>();
        p1Text.text = playerList[0].playerName;

        var p2Text = playersUI.transform.Find("P2").GetComponent<Text>();
        p2Text.text = playerList[1].playerName;

        var playersUINames = new Text[2] { p1Text, p2Text };

        playersUINames[defender].text += ": Defender";
    }

    private bool isCurrentClientSelectedAsDefender()
    {
        return defender == manager.client.connection.connectionId;
    }

    private bool isCurrentClientSelectedAsAttacker()
    {
        return attacker == manager.client.connection.connectionId;
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        print("WE ARE HERE!");
        if (isClient)
            return;

        RpcSetupPlayerDashboard();
    }
}
