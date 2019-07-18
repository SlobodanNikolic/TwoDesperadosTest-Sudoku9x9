using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudokuField : MonoBehaviour
{

    [SerializeField]
    private int _rowIndex;
    [SerializeField]
    private int _columnIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int rowIndex
    {
        get { return _rowIndex; }
        set { _rowIndex = value; }
    }

    public int columnIndex
    {
        get { return _columnIndex; }
        set { _columnIndex = value; }
    }
}
