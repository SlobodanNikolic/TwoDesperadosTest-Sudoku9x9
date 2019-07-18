using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public enum GAME_STATE{
        HOME = 0,
        READY = 1,
        PLAYING = 2,
        GAME_OVER = 3
    }

    private GAME_STATE currentState = GAME_STATE.HOME;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameState(GAME_STATE newState){
        switch(newState){

            case GAME_STATE.HOME:
                break;
            case GAME_STATE.READY:
                break;
            case GAME_STATE.PLAYING:
                break;
            case GAME_STATE.GAME_OVER:
                break;
        }
    }
}
