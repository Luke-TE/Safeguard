using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{    
    public static int _numOfCities = 5;
    public static float _drugCostPP;
    public static float _vaccineCostPP;

    private static float _diseaseCoeff = 0.5f;    
    
    static Settings()
    {
        _numOfCities = 5;
        _drugCostPP = 5;
        _vaccineCostPP = 15;
        _diseaseCoeff = 16;
    }

    public static float GetDiseaseCoeff()
    {
        return _diseaseCoeff;
    }

    public void SetDiseaseCoeff(string disease)
    {        
        switch (disease)
        {
            case "Measles":
                _diseaseCoeff = 16;
                break;
            case "HIV":
                _diseaseCoeff = 3;
                break;
            case "Ebola":
                _diseaseCoeff = 2;
                break;
            case "Smallpox":
                _diseaseCoeff = 6;
                break;
        }        
    }

    public void SetNumOfCities(int num)
    {
        _numOfCities = num;
    }

}
