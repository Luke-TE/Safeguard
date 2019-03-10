using System;


namespace healthHack
{
    public interface IModelInterface
    {
        void Update();
        bool setVaccine(float proportion);
        bool AddAntiVaxxer();
        bool setDrugTreatment(float proportion);
        float GetLastCost();
        float GetTotalCost();
        float GetInfected();
        float GetSusceptible();
        float GetDead();
        float GetRecovered();
        float GetTotalPopulation();

        void ExternalInfect(float quantity);
    }
}

