using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
  public void Spawn()
    {
        this.GetComponent<ThirdPersonController>().controller.Move(new Vector3(200, 200, 200));
    }
}
