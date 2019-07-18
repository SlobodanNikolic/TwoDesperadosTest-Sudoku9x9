using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SudokuControler : MonoBehaviour
{
  
    private int[][] mat;
    private int[][] solvedMat;
    [SerializeField]
    private int N = 9; // broj redova i kolona
    [SerializeField]
    private int SRN; // koren iz N
    [SerializeField]
    private int K = 30; // Koliko cifara uklanjamo sa table

    private GameObject[][] table;

    [SerializeField]
    GameObject[] sudokuButtons;

    [SerializeField]
    private Game game;

    private void Awake()
    {

    }

   

    

    private void Start()
    {

        InitializeTable();

        //Racunamo koren iz N
        Double SRNd = Math.Sqrt(N);
        SRN = Convert.ToInt32(SRNd);

        sudokuButtons = GameObject.FindGameObjectsWithTag("SudokuButton");
        int i = 0;
        int j = 0;

        foreach (GameObject sudBut in sudokuButtons)
        {
            //Debug.Log(sudBut);
            if (j!= 0 && ((j % 9) == 0))
            {
                i++;
                j = 0;
                table[i][j] = sudBut;
                sudBut.GetComponent<SudokuField>().rowIndex = i;
                sudBut.GetComponent<SudokuField>().columnIndex = j;
                j++;
            }
            else
            {
                table[i][j] = sudBut;
                sudBut.GetComponent<SudokuField>().rowIndex = i;
                sudBut.GetComponent<SudokuField>().columnIndex = j;

                j++;
            }
        }

        fillValues();

    }


    private void Initialize(){

        mat = new int[N][];
        table = new GameObject[N][];

        for (int i = 0; i < N; i++)
        {
            mat[i] = new int[N];
            table[i] = new GameObject[N];
        }
    }


    public void InitializeMatrix(){

        mat = new int[N][];
        for (int i = 0; i < N; i++)
        {
            mat[i] = new int[N];

        }
    }

    public void InitializeTable(){

        table = new GameObject[N][];

        for (int i = 0; i < N; i++)
        {
           
            table[i] = new GameObject[N];
        }
    }

    public void SetNewNumber(int rowIndex, int columnIndex, int number)
    {
        Debug.Log("Setting new number");
        if (game.isEasy())
        {
            Debug.Log("Game mode easy");

            if (isItSafe(rowIndex, columnIndex, number))
            {
                Debug.Log("It is safe");

                //upisi broj belom bojom
                mat[rowIndex][columnIndex] = number;
                table[rowIndex][columnIndex].GetComponentInChildren<Text>().text = number.ToString();
                table[rowIndex][columnIndex].GetComponentInChildren<Text>().color = Color.white;
            }
            else
            {
                Debug.Log("It is not safe");

                //upisi broj crvenom bojom. necemo ga pisati u matricu, jer mora da se promeni
                table[rowIndex][columnIndex].GetComponentInChildren<Text>().text = number.ToString();
                table[rowIndex][columnIndex].GetComponentInChildren<Text>().color = Color.red;
            }
        }
        else
        {
            Debug.Log("Game mode hard");

            //upisujemo broj u svakom slucaju, belom bojom
            mat[rowIndex][columnIndex] = number;
            table[rowIndex][columnIndex].GetComponentInChildren<Text>().text = number.ToString();
            table[rowIndex][columnIndex].GetComponentInChildren<Text>().color = Color.white;

        }
       
    }

    public bool isItSafe(int rowIndex, int columnIndex, int number){

        //proveri celom duzinom
        //pre
        for (int i = 0; i < rowIndex; i++){

            if(mat[i][columnIndex] == number){
                return false;
            }
        }
        //posle
        for (int i = rowIndex+1; i < N; i++)
        {

            if (mat[i][columnIndex] == number)
            {
                return false;
            }
        }

        //proveri sirinom
        //pre
        for (int j = 0; j < columnIndex; j++)
        {
            if (mat[rowIndex][j] == number)
            {
                return false;
            }
        }

        //posle
        for (int j = columnIndex + 1; j < N; j++)
        {
            if (mat[rowIndex][j] == number)
            {
                return false;
            }
        }

        //proveri u okrugu
        int i1 = rowIndex, i2 = rowIndex, j1 = columnIndex, j2 = columnIndex;

        while (i1 % 3 != 0)
            i1--;
        while (j1 % 3 != 0)
            j1--;
        while (i2 % 3 != 2)
            i2++;
        while (j2 % 3 != 2)
            j2++;
        for (int startI = i1; startI <= i2; startI++){
            for (int startJ = j1; startJ <= j2; startJ++){
                if (startI == rowIndex && startJ == columnIndex)
                    continue;
                if (mat[startI][startJ] == number)
                    return false;
            }
        }

        return true;
    }

    //private void Update()
    //{
    //    if(Input.GetKeyUp(KeyCode.S)){
    //        fillValues();
    //    }
    //    if(Input.GetKeyUp(KeyCode.P)){
    //        printSudoku();
    //    }
    //}

    // Sudoku Generator 
    public void fillValues()
    {

        InitializeMatrix();
        // Fill the diagonal of SRN x SRN matrices 
        fillDiagonal();

        // Fill remaining blocks 
        fillRemaining(0, SRN);
        solvedMat = mat;

        // Remove Randomly K digits to make game 
        removeKDigits();
        fillTable();
    }

    public void fillTable(){
        for (int i = 0; i < table.Length; i++){
            for (int j = 0; j < table[i].Length; j++){
                if (mat[i][j] != 0)
                {
                    table[i][j].GetComponentInChildren<Text>().text = mat[i][j].ToString();
                    table[i][j].GetComponent<Button>().interactable = false;
                    table[i][j].GetComponentInChildren<Text>().color = Color.gray;

                }
                else
                {
                    table[i][j].GetComponentInChildren<Text>().text = "";
                    table[i][j].GetComponentInChildren<Text>().color = Color.white;

                }
            }
        }
    }


    // Fill the diagonal SRN number of SRN x SRN matrices 
    void fillDiagonal()
    {

        for (int i = 0; i < N; i = i + SRN)

            // for diagonal box, start coordinates->i==j 
            fillBox(i, i);
    }

    // Returns false if given 3 x 3 block contains num. 
    Boolean unUsedInBox(int rowStart, int colStart, int num)
    {
        for (int i = 0; i < SRN; i++)
            for (int j = 0; j < SRN; j++)
                if (mat[rowStart + i][colStart + j] == num)
                    return false;

        return true;
    }

    // Fill a 3 x 3 matrix. 
    void fillBox(int row, int col)
    {
        int num;
        for (int i = 0; i < SRN; i++)
        {
            for (int j = 0; j < SRN; j++)
            {
                do
                {
                    num = randomGenerator(N);
                }
                while (!unUsedInBox(row, col, num));

                mat[row + i][col + j] = num;
            }
        }
    }

    // Random generator 
    int randomGenerator(int num)
    {
        System.Random r = new System.Random();
        return (int)Math.Floor((r.NextDouble() * num + 1));
    }

    // Check if safe to put in cell 
    Boolean CheckIfSafe(int i, int j, int num)
    {
        return (unUsedInRow(i, num) &&
                unUsedInCol(j, num) &&
                unUsedInBox(i - i % SRN, j - j % SRN, num));
    }

    // check in the row for existence 
    Boolean unUsedInRow(int i, int num)
    {
        for (int j = 0; j < N; j++)
            if (mat[i][j] == num)
                return false;
        return true;
    }

    // check in the row for existence 
    Boolean unUsedInCol(int j, int num)
    {
        for (int i = 0; i < N; i++)
            if (mat[i][j] == num)
                return false;
        return true;
    }

    // A recursive function to fill remaining  
    // matrix 
    Boolean fillRemaining(int i, int j)
    {

        if (j >= N && i < N - 1)
        {
            i = i + 1;
            j = 0;
        }
        if (i >= N && j >= N)
            return true;

        if (i < SRN)
        {
            if (j < SRN)
                j = SRN;
        }
        else if (i < N - SRN)
        {
            if (j == (int)(i / SRN) * SRN)
                j = j + SRN;
        }
        else
        {
            if (j == N - SRN)
            {
                i = i + 1;
                j = 0;
                if (i >= N)
                    return true;
            }
        }

        for (int num = 1; num <= N; num++)
        {
            if (CheckIfSafe(i, j, num))
            {
                mat[i][j] = num;
                if (fillRemaining(i, j + 1))
                    return true;

                mat[i][j] = 0;
            }
        }
        return false;
    }

    // Remove the K no. of digits to 
    // complete game 
    public void removeKDigits()
    {
        printSudoku();

        int count = K;
        while (count != 0)
        {
            int cellId = randomGenerator(N * N -1);

            // System.out.println(cellId); 
            // extract coordinates i  and j 
            int i = (cellId / N);
            int j = cellId % 9;
            if (j != 0)
                j = j - 1;

            // System.out.println(i+" "+j); 
            if (mat[i][j] != 0)
            {
                count--;
                mat[i][j] = 0;
            }
        }
    }

    // Print sudoku 
    public void printSudoku()
    {
        String line = "";
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
                line += (mat[i][j] + " ");
            Debug.Log(line);
            line = "";
        }
    }

    public void SetAllButtonsClickable(){
        for (int i = 0; i < table.Length; i++)
        {
            for (int j = 0; j < table[i].Length; j++)
            {
                if (mat[i][j] != 0)
                    table[i][j].GetComponent<Button>().interactable = true;
            }
        }
    }


}
