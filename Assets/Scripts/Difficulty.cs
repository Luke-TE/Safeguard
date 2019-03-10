using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{    
    private static int numOfCities = 5;
    private static double diseaseCoeff = 0.5;

    public static int GetNumOfCities()
    {
        return numOfCities;
    }

    public void SetNumOfCities(int value)
    {
        numOfCities = value;
    }

    public void SetDiseaseCoeff(string disease)
    {        
        switch (disease)
        {
            case "Measles":
                //diseaseCoeff = 
                break;
            case "HIV":
                //diseaseCoeff = 
                break;
            //case "Name3":
            //    diseaseCoeff =
            //    break;
            //case "Name4":
            //    diseaseCoeff =
            //    break;
        }        
    }
}
