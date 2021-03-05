using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    public Transform targetTransform;
    public float offsetMax = 20f;
    public float offsetMin = 10f;


    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.transform.position + offset;
        transform.LookAt(targetTransform);

        if (Input.mouseScrollDelta.y > 0 && offset.y < offsetMax)
        {
            offset.y += Input.mouseScrollDelta.y;
        }

        if (Input.mouseScrollDelta.y < 0 && offset.y > offsetMin)
        {
            offset.y += Input.mouseScrollDelta.y;
        }
    }
}
