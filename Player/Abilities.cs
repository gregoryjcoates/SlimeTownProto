using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{

    //ability unlocks
    public bool abilityOne = false;
    public bool abilityOneAdded = false;
    public bool abilityTwo = false;
    public bool abilityTwoAdded = false;
    public bool abilityThree = false;
    public bool abilityThreeAdded = false;
    public bool abilityFour = false;
    public bool abilityFourAdded = false;
    public bool abilityFive = false;
    public bool abilityFiveAdded = false;
    public Splitting splitting;



    //ability unlock function
    public void AbilitiesUnlock()
    {
        if (abilityOne == true & abilityOneAdded == false)
        {
            splitting = gameObject.AddComponent<Splitting>();
            abilityOneAdded = true;
            
        }
        if (abilityTwo == true)
        {

        }
        if (abilityThree == true)
        {

        }
        if (abilityFour == true)
        {

        }
        if (abilityFive == true)
        {

        }
    }
}
