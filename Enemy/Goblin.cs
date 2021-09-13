using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    CharacterController controller;
    // must have traits

    //inheritied from EnemyStats
    int damage = 0;
    float speed = 1f;
    int enemyHealth = 0;
    int slimeValue = 0;
    int dangerLevel = 0;
    float sphereDetectRange = 0;
    float sightDetectRange = 0;
    float attackRange = 0;
    //

    float moveTimer = 0f;
    float moveInterval = 1f;

    float attackTimer = 0f;
    float attackInterval = 1f;

    bool trapped = false;
    int playerLayerMask = 1 << 7;

    Vector3 playerLocation;
    Vector3 move = new Vector3(0, 0, 0);

    float fixedRoation = 0;
    float fixedPosition = 0;


    enum State
    {
        Deciding,
        Moving,
        Wandering,
        Attacking,
        Trapped,
        Distracted,
    }

    State activeState = State.Deciding;

    private void Awake()
    {
        EnemyStats enemyStats = gameObject.GetComponent<EnemyStats>();

        damage = enemyStats.damage;
        speed = enemyStats.speed;
        enemyHealth = enemyStats.enemyHealth;
        slimeValue = enemyStats.slimeValue;
        dangerLevel = enemyStats.dangerLevel;
        sphereDetectRange = enemyStats.sphereDetectRange;
        sightDetectRange = enemyStats.sightDetectRange;
        attackRange = enemyStats.attackRange;
    }

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();

    }

    private void Update()
    {
        if (activeState == State.Deciding)
        {
            DecideState();
        }

        IfActiveState();

        transform.eulerAngles = new Vector3(fixedRoation, transform.eulerAngles.y, fixedPosition);
    }


    // the brain of the enemy
    void DecideState()
    {
        // if not trapped allow actions
        if (trapped == false)
        {


            // if not trapped and a player is near
            if (SensePlayerInRange() | PlayerInFront().inFrontTrue == true)
            {


                // if not trapped and the player is within attack range
                if ((PlayerInFront().distance <= attackRange) & (PlayerInFront().distance != 0))
                {
                    activeState = State.Attacking;
                }
                else
                {
                    activeState = State.Moving;
                }
            }
            else
            {

                activeState = State.Wandering;
            }
        }
        else
        {
            activeState = State.Trapped;
        }
    }

    void IfActiveState()
    {
        if (activeState == State.Moving)
        {
            MovingState();
        }

        if (activeState == State.Wandering)
        {
            WanderingState();
        }

        if (activeState == State.Attacking)
        {
            AttackingState();
        }

        if (activeState == State.Trapped)
        {
            activeState = State.Deciding;
        }

        if (activeState == State.Distracted)
        {
            activeState = State.Deciding;
        }
    }

    void MovingState()
    {
        // move towards player


        Vector3 playerDirection = Vector3.RotateTowards(this.transform.position, playerLocation, 360f, 0f);


                // transform.rotation *= Quaternion.LookRotation(playerDirection);

                //need to somehow clamp angle 

                transform.LookAt(playerLocation);
            move = transform.forward;


                controller.Move(move * speed * Time.deltaTime);

               // if (PlayerInFront().inFrontTrue == true)
       
            
        
        activeState = State.Deciding;
    }

    void WanderingState()
    {
        // simply wander around
       

        int moveDirection = 0;

        if (Time.time > moveTimer)
        {
            //returns a random int between 0 and 3
            moveDirection = Random.Range(0, 4);
            transform.rotation *= Quaternion.Euler(0, Random.Range(-180f, 180f), 0);
            moveTimer = +moveInterval + Time.time;
        }


            move = transform.forward;
            controller.Move(move * speed * Time.deltaTime);


        activeState = State.Deciding;
    }

    void AttackingState()
    {
        //when in range attack the player

        if (PlayerInFront().player != null & Time.time >= attackTimer )
        {
            PlayerInFront().player.GetComponent<PlayerBasics>().health -= damage;
            attackTimer = Time.time + attackInterval;
        }

        activeState = State.Deciding;
    }


    void TrappedState()
    {
        // you are trapped and cannot move for x seconds
    }

    void DistractedState()
    {
        // you are enjoying a nice meal and thus cannot 
    }

    // if the player is within the radius sphereDetectRange of the enemy
    bool SensePlayerInRange()
    {
        bool hitPlayer = false;
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, sphereDetectRange,playerLayerMask);
        foreach (var hitCollider in hitColliders)
        {
            hitPlayer = true;
            playerLocation = hitCollider.transform.position;
        }

        if (hitPlayer == true)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    // if the player is directly in front the enemy within sightDetectRange
    (bool inFrontTrue,float distance,GameObject player) PlayerInFront()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, sightDetectRange, playerLayerMask) == true)
        {
            return (true,hit.distance,hit.transform.gameObject);
        }
        return (false,hit.distance,null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trap")
        {
            trapped = true;
        }
    }

  
}
