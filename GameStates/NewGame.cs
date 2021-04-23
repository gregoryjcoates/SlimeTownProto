using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour
{
    // Start is called before the first frame update



    // Update is called once per frame

    public void FreshGame()
    {

    }
    void Update()
    {

        // test code for creating new game scene. Currently uses the m key to set the texture values in mapcreation
        // also sets activestate in GameState to MapCreation thus calling paintTerrain;
        // later plan to execute code from gui meni
        if (Input.GetKey(KeyCode.M))
        {
            MapCreation mapCreation = gameObject.GetComponent<MapCreation>();
            mapCreation.texture1BottomLeft = 1;
            mapCreation.texture2TopLeft = 1;
            mapCreation.texture3BottomRight = 1;
            mapCreation.texture4TopRight = 1;
            GameState.GameStateInstance.activeState = GameState.State.MapCreation;
        }
    }
}
