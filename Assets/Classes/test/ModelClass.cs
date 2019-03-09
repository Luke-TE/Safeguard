using System;


namespace healthHack 
{
    public interface IModelInterface
    {
        void Update();
        bool setVaccine(float proportion);
        bool AddAntiVaxxer();
        bool setDrugTreatment(float proportion);
        float GetCosts();
        float GetInfected();
        float GetSubseptible();
        float GetDead();
        float GetRecovered();
    }
}

