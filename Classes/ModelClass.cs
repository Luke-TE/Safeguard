using System;
namespace Application
{
    public interface ModelClass
    {
        public void update();
        public bool addVacine();
        public bool addAntiVaxxer();
        public bool drugPopulation();
        public double getCosts();
        public double getInfected();
        public double getSubseptible();
        public double getDead();
        public double getRecovered();
    }
}
