using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    public Transform targetTransform;


    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.transform.position + offset;
        transform.LookAt(targetTransform);
    }
}
