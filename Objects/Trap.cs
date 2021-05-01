using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    bool trapSprung = false;
    float trapTimer = 0;
    bool timerEnded = true;
    public float trapDuration = 10f;
    // Update is called once per frame
    private void Update()
    {
        if (trapSprung == true & timerEnded == true)
        {
            trapTimer = trapDuration;
            timerEnded = false;
        }
        else if (trapSprung == true & timerEnded == false)
        {
            trapTimer -= Time.deltaTime;
            if (trapTimer <= 0)
            {
                trapTimer = 0;
                trapSprung = false;
                timerEnded = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" & trapSprung == false)
        {
            Debug.Log("trap sprung");
            other.gameObject.GetComponent<Enemy2>().Trapped(true);
            trapSprung = true;
        }
    }
  //  private void OnCollisionEnter(Collision collision)
  //  {
   //     Debug.Log("collision happened");
   //     if (collision.gameObject.tag =="Enemy" & trapSprung == false)
    //    {
    //        Debug.Log("trap sprung");
     //       collision.gameObject.GetComponent<Enemy2>().Trapped(true);
     //       trapSprung = true;
      //  }
 //   }
}
