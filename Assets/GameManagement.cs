﻿using UnityEngine;
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

    void Awake()
    {
        //DontDestroyOnLoad(this);
    }

    void Start()
    {
        var lobbyManager = GameObject.Find("LobbyManager");
        var manager = lobbyManager.GetComponent<LobbyManager>();
        RegisterManager(manager);

        RegisterPlayers(lobbyManager.GetComponentsInChildren<LobbyPlayer>(true));

        defender = DrawDefender();
        attacker = (defender + 1) % 2;

        SetupPlayerDashboard();

        FlipDefenderCamera();

        if (defender == manager.client.connection.connectionId)
        {
            CmdPrepareDefender();
        }
        else if (attacker == manager.client.connection.connectionId)
        {
            PrepareAttacker();
        }
    }

    [Command]
    public void CmdPrepareDefender()
    {
        var go = (GameObject)Instantiate(
     manager.spawnPrefabs[0],
   transform.position + new Vector3(3, 3, 0),
 Quaternion.identity);

        go.GetComponent<NetworkIdentity>().localPlayerAuthority = true;
        NetworkServer.Spawn(go);
    }

    public void PrepareAttacker()
    {

    }

    private void FlipDefenderCamera()
    {
        if (defender == manager.client.connection.connectionId)
        {
            Camera.main.transform.Rotate(new Vector3(0, 0, 180));
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

    private void SetupPlayerDashboard()
    {
        var p1Text = playersUI.transform.Find("P1").GetComponent<Text>();
        p1Text.text = playerList[0].playerName;

        var p2Text = playersUI.transform.Find("P2").GetComponent<Text>();
        p2Text.text = playerList[1].playerName;

        var playersUINames = new Text[2] { p1Text, p2Text };

        playersUINames[defender].text += ": Defender";
    }
}
