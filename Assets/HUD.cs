using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;

public class HUD : NetworkBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetupHUD(List<NetworkConnection> players)
    {

        //HUD = GameObject.Find("HUD");
        var playersUI = transform.GetComponentsInChildren<Text>();
        playersUI[0].text = "Player 1:" + players[0].connectionId;
        playersUI[1].text = "Player 2:" + players[1].connectionId;
    }
}
