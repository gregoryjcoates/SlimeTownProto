using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnCircle : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, this.GetComponent<SphereCollider>().radius);
    }
    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(collision.gameObject.name);

            MonoBehaviour[] scripts = collision.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = true;
            }


            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        MonoBehaviour[] scripts = other.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }

    }
}

