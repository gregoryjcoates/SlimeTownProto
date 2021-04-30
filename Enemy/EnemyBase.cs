using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    // the base class for enemies to inherit

    bool inAttackRange = false;

    // Enemy controller
    bool moved = false;
    float moveTimer = 0;
    bool timerEnded =true;
    CharacterController controller;


    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }
    //private void Update()
 //   {
   //     EnemyBrain();
  //      PlayerNear();
  //  }
    // Where the enemy decides what to do based
    public void EnemyBrain(int damage, int speed)
    {
        if (moved == false)
        {
            if (PathBlocked() == false)
            {
                controller.Move(Movement());
                moved = true;
            }
            else if (PathBlocked()== true & inAttackRange == true)
            {
                AttackPlayer(damage);
                inAttackRange = false;

            }
            else if (PathBlocked() == true &inAttackRange == false)
            {

                gameObject.transform.Rotate(0,90,0);
            }

        }
        else if (moved == true & timerEnded == true)
        {
            moveTimer = Random.Range(0.3f, 1f);
            timerEnded = false;
        }
        else if (moved == true & timerEnded == false)
        {
            moveTimer -= Time.deltaTime;
            if (moveTimer <= 0)
            {
                timerEnded = true;
                moved = false;
            }
        }
    }

    // uses a ray to detect player
   public bool PlayerNear(float distance = 10)
    {
        int layerMask = 1;
        RaycastHit hit;

        if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out hit, distance,layerMask))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }

    // detects within close range whether the plaer is nearby or if its other obstacles 
    public bool PathBlocked()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;

        if (PlayerNear(2) == true)
        {
            inAttackRange = true;
            return true;
        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2, layerMask) == true & PlayerNear() == false)
        {
            return true;
        }
     else
        {
            return false;
        }
    }

    //sets controller.Move
    public Vector3 Movement()
    {
        Vector3 move = new Vector3(0, 0, 0);

        if (PlayerNear() == true)
        {
            move = new Vector3(0, -10f, 1f);
            return move;
        }
        else
        {
            return move;
        }

    }
    // does damage to player
    void AttackPlayer(int damage)
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            if (hit.collider.gameObject.tag == "Player")
            {
                GameObject a = hit.collider.gameObject;
                a.GetComponent<PlayerBasics>().health -= damage;
            }
        }
    }

    // on death sets object to inactive state and awards slime to player
    public void Death(int enemyHealth, int slimeValue)
    {
        if (enemyHealth <= 0)
        {
            gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBasics>().slimeMass += slimeValue;
        }
    }

}
