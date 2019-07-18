using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickControler : MonoBehaviour
{
    [SerializeField]
    private GameObject selectedField;
    [SerializeField]
    private GameObject selectedNumber;
    [SerializeField]
    private SudokuControler sudoku;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClicked(){

        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
        if (selectedObject != null)
        {
         //Selektovali smo dugme
            Debug.Log("Clicked on : " + selectedObject.name);


            if(selectedObject.tag == "SudokuButton"){

                selectedField = selectedObject;

            }else if(selectedObject.tag == "NumericButton"){

                selectedNumber = selectedObject;
                //Logika
                if (selectedField != null)
                {
                    int number;
                    int.TryParse(selectedNumber.name, out number);

                    SudokuField fieldInfo = selectedField.GetComponent<SudokuField>();

                    if(number != 0){
                        sudoku.SetNewNumber(fieldInfo.rowIndex, fieldInfo.columnIndex, number);
                    }
                    //Text textComponent = selectedField.GetComponentInChildren<Text>();
                    //if (textComponent != null)
                    //{
                    //    textComponent.text = selectedNumber.name;
                        
                    //}
                }
            }

        }

    }
}
