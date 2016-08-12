using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using Prototype.NetworkLobby;

public class GameManagement : NetworkBehaviour
{
    public NetworkLobbyPlayer[] playerList;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void RegisterPlayers(NetworkLobbyPlayer[] players)
    {
        this.playerList = players;
    }
}
