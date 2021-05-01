using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasics : MonoBehaviour
{
    
    
    //This defines that basic variables of the player such as health and other variables

    public int health = 100;

    // slimeMass is the value for the growth system. Eating enemies increases value
    public int slimeMass = 0;
    private int slimeMassOld = 0;
    // Growth System basics
    int growthLevelOne = 1;
    int growthLevelTwo = 5;
    int growthLevelThree = 10;
    int growthLevelFour = 15;
    int growthLevelFive = 20;


    // Growth Level triggers
    void Leveling()
    {
        if (slimeMass >= growthLevelOne)
        {
            //do thing on levelup
            gameObject.GetComponent<Abilities>().abilityOne = true;
        }
        if (slimeMass >= growthLevelTwo)
        {
            //do thing on levelup
            gameObject.GetComponent<Abilities>().abilityTwo = true;
        }
        if (slimeMass >= growthLevelThree)
        {
            //do thing on levelup
            gameObject.GetComponent<Abilities>().abilityThree = true;
        }
        if (slimeMass >= growthLevelFour)
        {
            //do thing on levelup
            gameObject.GetComponent<Abilities>().abilityFour = true;
        }
        if (slimeMass >= growthLevelFive)
        {
            //do thing on levelup
            gameObject.GetComponent<Abilities>().abilityFive = true;
        }
    }

    private void Update()
    {
        Leveling();
        if (slimeMass != slimeMassOld)
        {
            slimeMassOld = slimeMass;
            gameObject.GetComponent<Abilities>().AbilitiesUnlock();
        }
    }
}
