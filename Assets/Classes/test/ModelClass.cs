using System;


namespace healthHack 
{
    public interface IModelInterface
    {
        void Update();
        bool AddVacine();
        bool AddAntiVaxxer();
        bool DrugPopulation();
        double GetCosts();
        double GetInfected();
        double GetSubseptible();
        double GetDead();
        double GetRecovered();
    }
}

