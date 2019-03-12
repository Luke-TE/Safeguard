using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace healthHack
{
    public class City : MonoBehaviour
    {             
        private static System.Random randomGenerator;        

        private int startPopulation;
        private int infectionLimit;

        private IModel model;

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

            model = new SIRModel(1000000, 0, 0.5f, 14, 7);
        }

        public bool IsDiseaseSpreading(City city)
        {
            double prob = randomGenerator.NextDouble();
            if (model.GetInfectedPopulation() / model.GetTotalPopulation() > prob)
            {
                city.InfectPopulation();
                return true;
            }
            return false;
        }        

        public void InfectPopulation()
        {
            model.InfectPopulation(1);
        }

        public void InfectPopulation(float amount)
        {
            model.InfectPopulation(amount);
        }

        public void SetProportionVaccinated(float proportion)
        {
            model.SetProportionVaccinated(proportion);
        }
        public void SetProportionTreated(float proportion)
        {
            model.SetProportionTreated(proportion);
        }

        public IModel GetModel()
        {
            return model;
        } 
    }
}
