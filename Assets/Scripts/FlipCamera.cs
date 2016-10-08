using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class FlipCamera : NetworkBehaviour
{
    void Start()
    { }

    public override void OnStartLocalPlayer()
    {
    }

    public override void OnStartAuthority()
    {
        print(hasAuthority);

        print("flipper started...");
        Camera.main.transform.Rotate(new Vector3(0, 0, 180));
    }
}
