using System;


namespace healthHack 
{
    public interface IModelInterface
    {
        void Update();
        bool setVaccine(float proportion);
        bool AddAntiVaxxer();
        bool setDrugTreatment(float proportion);
        double GetCosts();
        double GetInfected();
        double GetSubseptible();
        double GetDead();
        double GetRecovered();
    }
}

