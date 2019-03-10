using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{    
    private static int numOfCities = 5;
    private static float diseaseCoeff = 0.5f;

    public static int GetNumOfCities()
    {
        return numOfCities;
    }

    public void SetNumOfCities(int value)
    {
        numOfCities = value;
        Debug.Log(numOfCities);
    }

    public void SetDiseaseCoeff(string disease)
    {        
        switch (disease)
        {
            case "Measles":
                diseaseCoeff = 16;
                break;
            case "HIV":
                diseaseCoeff = 3;
                break;
            case "Ebola":
                diseaseCoeff = 2;
                break;
            case "Smallpox":
                diseaseCoeff = 6;
                break;
        }        
    }


    public static float getdis()
    {
        return diseaseCoeff;
    }
}
