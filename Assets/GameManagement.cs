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

    private int defender;
    private NetworkManager manager;

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

        SetupPlayerDashboard();

        FlipAttackerCamera();

        //print(manager.client.connection.connectionId);

        //playerList[0].netId;
       
        //print(manager.client.connection.connectionId);
        //print(playerList[0].netId);
        //print(playerList[0].playerControllerId);
        // GetComponentInChildren<Text>().text = manager.client.connection.connectionId + "-" + playerList[0].netId + " -" + playerList[0].playerControllerId;
    }

    private void FlipAttackerCamera()
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
