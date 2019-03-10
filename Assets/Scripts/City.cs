using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace healthHack
{
    public class City : MonoBehaviour
    {             
        private int startPopulation;
        private int infectionLimit;
        private static System.Random randomGenerator;        

        void Start()
        {            
            randomGenerator = new System.Random();

            startPopulation = randomGenerator.Next(100000, 10000000);
            var infectionPopulationRandom = randomGenerator.NextDouble();

            while (infectionPopulationRandom < 0.4)
            {
                infectionPopulationRandom = randomGenerator.NextDouble();
            }

            infectionLimit = (int)Math.Ceiling(infectionPopulationRandom * startPopulation);            
        }        
    }
}
