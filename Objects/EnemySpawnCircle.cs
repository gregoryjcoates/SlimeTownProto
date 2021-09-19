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
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(other.gameObject.name);

            MonoBehaviour[] scripts = other.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = true;
            }

            MeshRenderer[] meshes = other.GetComponents<MeshRenderer>();

            foreach(MeshRenderer mesh in meshes)
            {
                mesh.enabled = true;
            }
            
        }

        if (other.gameObject.CompareTag("EnviroObject"))
        {


            MeshRenderer[] meshes = other.GetComponents<MeshRenderer>();

            foreach (MeshRenderer mesh in meshes)
            {
                mesh.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            MonoBehaviour[] scripts = other.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }

            MeshRenderer[] meshes = other.GetComponents<MeshRenderer>();

            foreach (MeshRenderer mesh in meshes)
            {
                mesh.enabled = false;
            }
        }

        if (other.gameObject.CompareTag("EnviroObject"))
        {


            MeshRenderer[] meshes = other.GetComponents<MeshRenderer>();

            foreach (MeshRenderer mesh in meshes)
            {
                mesh.enabled = false;
            }
        }

    }
}

