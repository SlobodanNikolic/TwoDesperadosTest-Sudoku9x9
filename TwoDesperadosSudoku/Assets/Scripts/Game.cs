using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public enum GAME_STATE{
        HOME = 0,
        READY = 1,
        PLAYING = 2,
        GAME_OVER = 3
    }

    public enum GAME_MODE{
        EASY = 0,
        HARD = 1
    }

    [SerializeField]
    private GAME_STATE currentState = GAME_STATE.HOME;
    [SerializeField]
    private GAME_MODE currentDifficulty = GAME_MODE.EASY;
    [SerializeField]
    private Sprite doorEasyOpen;
    [SerializeField]
    private Sprite doorEasyClosed;
    [SerializeField]
    private Sprite doorHardOpen;
    [SerializeField]
    private Sprite doorHardClosed;
    [SerializeField]
    private Button leverButton;
    [SerializeField]
    private Button doorButtonEasy;
    [SerializeField]
    private Button doorButtonHard;

    [SerializeField]
    private GameObject homePanel;

    [SerializeField]
    private GameObject gamePanel;

    [SerializeField]
    private GameObject gameOverPanel;

    private SudokuControler sudoku;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDifficulty(){
        if(currentDifficulty == GAME_MODE.EASY){

            currentDifficulty = GAME_MODE.HARD;
            doorButtonHard.gameObject.SetActive(true);
            doorButtonEasy.gameObject.SetActive(false);

        }
        else
        {
            currentDifficulty = GAME_MODE.EASY;
            doorButtonHard.gameObject.SetActive(false);
            doorButtonEasy.gameObject.SetActive(true);

        }
    }

    public void Play(){
        Invoke("Ready", 1f);
    }

    public void SetGameState(GAME_STATE newState){
        switch(newState){

            case GAME_STATE.HOME:
                homePanel.SetActive(true);
                gameOverPanel.SetActive(false);
                //gamePanel.SetActive(false);
                break;
            case GAME_STATE.READY:
                homePanel.SetActive(false);
                gameOverPanel.SetActive(false);
                gamePanel.SetActive(true);
                break;
            case GAME_STATE.PLAYING:
                break;
            case GAME_STATE.GAME_OVER:
                homePanel.SetActive(false);
                gameOverPanel.SetActive(true);
                sudoku.fillValues();
                //gamePanel.SetActive(false);
                break;
        }
    }

    public void Ready(){
        SetGameState(GAME_STATE.READY);
    }
}
