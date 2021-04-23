using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState GameStateInstance;

    public enum State
    {
        MainMenu,
        NewGame,
        MapCreation,
        Playing,
        Paused,
        GameOver,

    }
    private void Start()
    {
        GameStateInstance = this;
    }

   public State activeState = State.MainMenu;
    
    // Start is called before the first frame update
    void Awake()
    {
        // checks if instance is already created and destroys new instance if so
        if (GameStateInstance != null && GameStateInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            GameStateInstance = this;
        }
       // State activeState = State.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeState == State.MainMenu)
        {
            //MainMenu;
        }

        if (activeState == State.NewGame)
        {
            //  NewGame;
            Debug.Log("state is set to NewGame");
            //test code to set/reset player position. Because CharacterController is used must use move
            ThirdPersonController player = GameObject.FindWithTag("Player").GetComponent<ThirdPersonController>();
            player.controller.Move(new Vector3(200,200,200));

            activeState = State.Playing;
        }

        if (activeState == State.MapCreation)
        {
            // MapCreation;

            Debug.Log("game state is set to map creation");
            MapCreation mapCreation = gameObject.GetComponent<MapCreation>();
            mapCreation.PaintTerrain();

            activeState = State.Playing;
        }
        
        if (activeState == State.Playing)
        {
           // Playing;
        }

        if (activeState == State.Paused)
        {
           // Paused;
        }

        if (activeState == State.GameOver)
        {
           // GameOver;
        }

    }
}
