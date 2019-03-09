using System;

namespace healthHack
{
    public class City
    {
        //private SIRModel model;
        private string name;
        
        //private Board board;

        private int START_POPULATION;
        private int INFECTION_LIMIT;
        private Random randomGenerator; 


        public City(string name)
        {
            this.name = name;
            randomGenerator = new Random();
            var populationRandom = randomGenerator.Next(100000, 10000000);
            this.START_POPULATION = populationRandom;
            var infectionPopulationRandom = randomGenerator.NextDouble();
           while (infectionPopulationRandom < 0.4)
            {
                infectionPopulationRandom = randomGenerator.NextDouble();
            }

            INFECTION_LIMIT = (int) Math.Ceiling(infectionPopulationRandom * START_POPULATION);
            Console.WriteLine("Starting Population: " + START_POPULATION);
            Console.WriteLine("Infection Limit: " + INFECTION_LIMIT);
           
        }

        public string toString()
        {
            return name;
        }

    }
}
