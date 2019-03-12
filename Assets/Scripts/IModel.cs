using System;


namespace healthHack
{
    public interface IModel
    {
        void Update();
        bool SetProportionVaccinated(float proportion);
        bool SetProportionTreated(float proportion);

        bool AddAntiVaxxer();
        float GetLastCost();
        float GetTotalCost();
        float GetInfectedPopulation();
        float GetSusceptiblePopulation();
        float GetDead();
        float GetRecovered();
        float GetTotalPopulation();

        void InfectPopulation(float quantity);
    }
}

