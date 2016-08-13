using UnityEngine;
using System.Collections;
using Prototype.NetworkLobby;

public class Attacker : MonoBehaviour
{

    private LobbyManager manager;

    // Use this for initialization
    void Start()
    {
        var lobbyManager = GameObject.Find("LobbyManager");
        manager = lobbyManager.GetComponent<LobbyManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
