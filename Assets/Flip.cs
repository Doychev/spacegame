using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Flip : NetworkBehaviour
{
    bool flipped = false;
    // Use this for initialization
    void Start()
    {

        //    if (hasAuthority)
        //    {
        //
        //        print("flipper started...");
        //        Camera.main.transform.Rotate(new Vector3(0, 0, 180));
        //
        //    }

    }

    // public override void OnStartClient()
    // {
    //
    //     print("On start client...");
    //
    //     if (hasAuthority)
    //     {
    //
    //         print("flipper started...");
    //         Camera.main.transform.Rotate(new Vector3(0, 0, 180));
    //
    //     }
    // }

    public override void OnStartLocalPlayer()
    {
        //print("On start OnStartLocalPlayer...");
        //
        //if (hasAuthority)
        //{
        //
        //    print("flipper started...");
        //    Camera.main.transform.Rotate(new Vector3(0, 0, 180));
        //
        //}
    }

    public override void OnStartAuthority()
    {
        print(hasAuthority);

        print("flipper started...");
        Camera.main.transform.Rotate(new Vector3(0, 0, 180));

    }

    void Update()
    {
        //  if (hasAuthority)
        //  {
        //
        //      print("flipper update...");
        //      Camera.main.transform.Rotate(new Vector3(0, 0, 180));
        //
        //  }
    }

}
