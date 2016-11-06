using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;

public class HUD : NetworkBehaviour
{
    Transform playersHolder;

    Text genericMessage;

    [SyncVar(hook = "SetupHUDp1")]
    public string p1Info = "p1 info placeholder";

    [SyncVar(hook = "SetupHUDp2")]
    public string p2Info = "p2 info placeholder";

    void Start()
    {
        playersHolder = transform.FindChild("Players");
        genericMessage = transform.FindChild("GenericMessage").GetComponent<Text>();
    }

    public void SetupHUDp1(string info)
    {
        var playersUI = playersHolder.GetComponentsInChildren<Text>();
        playersUI[0].text = info;
    }

    public void SetupHUDp2(string info)
    {
        var playersUI = playersHolder.GetComponentsInChildren<Text>();
        playersUI[1].text = info;
    }

    public void ShowGenericMessage(string message)
    {
        genericMessage.enabled = true;
        genericMessage.text = message;
        Invoke("HideGenericMessage", 3);
    }

    private void HideGenericMessage()
    {
        genericMessage.enabled = false;
    }
}
