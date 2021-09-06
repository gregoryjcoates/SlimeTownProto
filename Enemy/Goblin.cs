using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    CharacterController controller;
    // must have traits

    //inheritied from EnemyStats
    int damage = 0;
    int speed = 0;
    int enemyHealth = 0;
    int slimeValue = 0;
    int dangerLevel = 0;
    float sphereDetectRange = 0;
    float sightDetectRange = 0;
    float attackRange = 0;
    //

    bool moved = false;
    float moveTimer = 0;
    float moveInterval = 1f;
    bool timerEnded = true;

    bool trapped = false;
    int playerLayerMask = 1 << 7;

    Vector3 playerLocation;
    Vector3 move = new Vector3(0, 0, 0);

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
        damage = gameObject.GetComponent<EnemyStats>().damage;
        speed = gameObject.GetComponent<EnemyStats>().speed;
        enemyHealth = gameObject.GetComponent<EnemyStats>().enemyHealth;
        slimeValue = gameObject.GetComponent<EnemyStats>().slimeValue;
        dangerLevel = gameObject.GetComponent<EnemyStats>().dangerLevel;
        sphereDetectRange = gameObject.GetComponent<EnemyStats>().sphereDetectRange;
        sightDetectRange = gameObject.GetComponent<EnemyStats>().sightDetectRange;
        attackRange = gameObject.GetComponent<EnemyStats>().sightDetectRange;

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
        Debug.Log(activeState);
        IfActiveState();

    }


    // the brain of the enemy
    void DecideState()
    {
        // if not trapped allow actions
        if (trapped == false)
        {
            Debug.Log("not trapped");

            // if not trapped and a player is near
            if (SensePlayerInRange() | PlayerInFront().inFrontTrue == true)
            {
                Debug.Log("player in range");

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
                Debug.Log("wandering state is called");
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
            MoveTimer();

            Vector3 playerDirection =Vector3.RotateTowards(this.transform.position,playerLocation,360f,0f);

            if (timerEnded == true)
            {
                for (int i = 0; i < 1;)
                {
                    Debug.Log(playerLocation);
                    // transform.rotation *= Quaternion.LookRotation(playerDirection);

                    //need to somehow clamp angle 
                    transform.LookAt(playerLocation);
                    move = transform.forward;
                    controller.Move(move);

                    if (PlayerInFront().inFrontTrue == true)
                    {
                        timerEnded = false;
                        i++;
                    }
                    else
                    {

                    }
                }
            }
            activeState = State.Deciding;
        }

        if (activeState == State.Wandering)
        {
            MoveTimer();

            if (timerEnded == true)
            {
                //returns a random int between 0 and 3
                int moveDirection = Random.Range(0, 4);

                if (moveDirection == 0)
                {
                    //move forward
                }
                else if (moveDirection == 1)
                {
                    // move left
                    transform.rotation *= Quaternion.Euler(0, -90, 0);
                    //transform.rotation = Quaternion.Slerp(transform.rotation,);

                }
                else if (moveDirection == 2)
                {
                    //move right
                    transform.rotation *= Quaternion.Euler(0, 90, 0);
                }
                else
                {
                    // move backward
                    transform.rotation *= Quaternion.Euler(0, 180, 0);
                }

                move = transform.forward;
                controller.Move(move);

                timerEnded = false;
            }

            activeState = State.Deciding;
        }

        if (activeState == State.Attacking)
        {
            activeState = State.Deciding;
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
    }

    void WanderingState()
    {
        // simply wander around
    }

    void AttackingState()
    {
        //when in range attack the player
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
    (bool inFrontTrue,float distance) PlayerInFront()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, sightDetectRange, playerLayerMask) == true)
        {
            return (true,hit.distance);
        }
        return (false,hit.distance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trap")
        {
            trapped = true;
        }
    }

    void MoveTimer()
    {
        if (Time.time > moveTimer)
        {
            moveTimer = Time.time + moveInterval;
            timerEnded = true;
        }
    }
}
