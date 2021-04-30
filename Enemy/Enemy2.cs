using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyBase 
{

    // enemy inheritance test
    public int damage = 10;
    public int speed = 2;
    public int enemyHealth = 5;
    public int slimeValue = 1;
    public void Update()
    {
        EnemyBrain(damage, speed);
        PlayerNear();
        Death(enemyHealth, slimeValue);
        if (PlayerNear() == true)
        {
            Debug.Log(damage);
        }
    }
}
