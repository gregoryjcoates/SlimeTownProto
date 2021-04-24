using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpSystem : MonoBehaviour
{
    // create a warp system to later trigger an animation, and for now to trigger warping between locations

    //  public Vector3 warpLocation = new Vector3;

    public Vector3 warpTarget = new Vector3(0f, 0f, 0f);

    private void OnTriggerEnter(Collider other)
    {
        Warp();
    }

    public void Warp()
    {

        ThirdPersonController player = GameObject.FindWithTag("Player").GetComponent<ThirdPersonController>();
        player.controller.Move(warpTarget - player.playerLocation);
    }
}
