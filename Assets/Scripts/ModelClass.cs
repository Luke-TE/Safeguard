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
        float GetSubseptible();
        float GetDead();
        float GetRecovered();

        void ExternalInfect(float quantity);
    }
}

