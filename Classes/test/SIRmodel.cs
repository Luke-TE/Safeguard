
using System;
namespace healthHack
{
    public class SIRModel
    {
        private float MAX_POPULATION;
        private float susceptible;
        private float infected_U;
        private float infected_TR;
        private float recovered;
        private float vaccinated;

        private float treatment_reduction;
        private float background_transmission_rate;
        private int recovery_untreated;
        private int recovery_treated;

        public SIRModel()
        {

        }

        public float change_in_susceptible()
        {
            return -1 * this.background_transmission_rate * this.susceptible * (this.infected_U + this.treatment_reduction * this.infected_TR);
        }

        public float getProportionTreated()
        {
            return 0;
        }

        public float change_in_infected_U()
        {
            return this.background_transmission_rate * this.susceptible * (this.infected_U + this.treatment_reduction * this.infected_TR) * (1 - this.getProportionTreated()) - (1 / this.recovery_untreated) * this.infected_U;
        }

        public float change_in_infected_TR()
        {
            return this.background_transmission_rate * this.susceptible * (this.infected_U + this.treatment_reduction * this.infected_TR) * this.getProportionTreated() - (1 / this.recovery_treated) * this.infected_TR;
        }

        public float change_in_recovered()
        {
            return (1 / this.recovery_treated) * this.infected_TR + (1 / this.recovery_untreated) * this.infected_U;
        }

        public void update()
        {
            float s = this.change_in_susceptible();
            float i_u = this.change_in_infected_U();
            float i_tr = this.change_in_infected_TR();
            float r = this.change_in_recovered();

            this.susceptible += s;
            this.infected_U += i_u;
            this.infected_TR += i_tr;
            this.recovered += r;
        }
    }
}

