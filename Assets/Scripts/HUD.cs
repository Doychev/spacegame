using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;

public class HUD : NetworkBehaviour
{
    [SyncVar(hook = "SetupHUDp1")]
    public string p1Info = "p1 info placeholder";

    [SyncVar(hook = "SetupHUDp2")]
    public string p2Info = "p2 info placeholder";

    public void SetupHUDp1(string info)
    {
        var playersUI = transform.GetComponentsInChildren<Text>();
        playersUI[0].text = info;
    }

    public void SetupHUDp2(string info)
    {
        var playersUI = transform.GetComponentsInChildren<Text>();
        playersUI[1].text = info;
    }
}
