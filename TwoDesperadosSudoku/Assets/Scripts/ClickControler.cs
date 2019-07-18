﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickControler : MonoBehaviour
{

    private GameObject selectedField;
    private GameObject selectedNumber;

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
            }

        }

    }
}
