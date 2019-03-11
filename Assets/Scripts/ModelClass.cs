using System;


namespace healthHack
{
    public interface IModelInterface
    {
        void Update();
        bool SetProportionVaccinated(float proportion);
        bool AddAntiVaxxer();
        bool SetProportionTreated(float proportion);
        float GetLastCost();
        float GetTotalCost();
        float GetInfectedPopulation();
        float GetSusceptiblePopulation();
        float GetDead();
        float GetRecovered();
        float GetTotalPopulation();

        void ExternalInfect(float quantity);
    }
}

