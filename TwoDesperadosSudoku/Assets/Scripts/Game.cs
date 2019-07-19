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

    private int score;

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

    [SerializeField]
    private SudokuControler sudoku;

    [SerializeField]
    private Text youTriedScore;

    [SerializeField]
    private Text correctFieldsScore;

   
    [SerializeField]
    private Text totalScore;

    [SerializeField]
    private GameObject scoreLabels;

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
            sudoku.setK(40);
            sudoku.ResetValues();

        }
        else
        {
            currentDifficulty = GAME_MODE.EASY;
            doorButtonHard.gameObject.SetActive(false);
            doorButtonEasy.gameObject.SetActive(true);
            sudoku.setK(25);
            sudoku.ResetValues();
        }
    }

    public void Play(){
        score = 0;
        Invoke("Ready", 1f);
    }

    public void GameOver(){
        CalculateScore();
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
                //set all labels

                break;
        }
    }

    private void SetLabels(int youTried, int correct, int total){
        scoreLabels.SetActive(true);

        totalScore.text = total.ToString();
        youTriedScore.text = youTried.ToString();
        correctFieldsScore.text = correct + " x 100";
    }

    private void GoToHome(){
        SetGameState(GAME_STATE.HOME);
    }

    private void GoToGameOver(){
        SetGameState(GAME_STATE.GAME_OVER);
    }

    public bool isEasy(){
        if (currentDifficulty == GAME_MODE.EASY)
            return true;
        else return false;
    }

    public void Ready(){
        SetGameState(GAME_STATE.READY);
    }

    private void CalculateScore(){
        int correctFields = sudoku.getK() - sudoku.missingFieldsCount();
        int youTried = 1500;
        score = youTried + correctFields * 100;
        SetLabels(youTried, correctFields, score);
        Invoke("GoToGameOver", 0.5f);

    }
}
