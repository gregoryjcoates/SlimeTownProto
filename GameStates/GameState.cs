using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance = new GameState();

    enum State
    {
        MainMenu,
        NewGame,
        MapCreation,
        Playing,
        Paused,
        GameOver,

    }

    State activeState = State.MainMenu;
    
    // Start is called before the first frame update
    void Awake()
    {
        State activeState = State.MainMenu;
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
        }

        if (activeState == State.MapCreation)
        {
           // MapCreation;    
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
