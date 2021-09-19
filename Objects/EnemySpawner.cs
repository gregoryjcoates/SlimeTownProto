using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  
    public Vector3 enemyLocation = new Vector3(0,0,0);
    public GameObject enemyType = null;

    public void SpawnEnemy()
    {
        Instantiate(enemyType,enemyLocation,Quaternion.identity);
    }

}
