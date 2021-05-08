using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoActivator : MonoBehaviour
{

    // Start is called before the first frame update
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
