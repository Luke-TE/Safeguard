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

        private IModelInterface model;
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

            model = new SIRModel(startPopulation, 5, 0.5f, 0.00001f, 14, 7);
        }

        public bool spreads(City city)
        {
            double prob = randomGenerator.NextDouble();
            if (this.model.GetInfected() / startPopulation > prob)
            {
                city.infectPopulation();
                return true;
            }
            return false;
        }

        public void Update()
        {
            model.Update();
        }

        public void infectPopulation()
        {
            model.ExternalInfect(1);
        }

        public void infectPopulation(float amount)
        {
            model.ExternalInfect(amount);
        }

        public void setVaccineLevel(float proportion)
        {
            model.setVaccine(proportion);
        }
        public void setDrugTreatment(float proportion)
        {
            model.setDrugTreatment(proportion);
        }

        public IModelInterface getModel()
        {
            return this.model;
        }

    }
}
