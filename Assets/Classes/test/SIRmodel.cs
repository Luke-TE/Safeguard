
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




        private int time;
        private float proportion_treated;
        private float proportion_vaccinated;

        private float treatment_reduction;
        private float background_transmission_rate;
        private float recovery_untreated;
        private float recovery_treated;

        public SIRModel(float susceptible, float infected_U, float treatment_reduction, float background_transmission_rate, int recovery_untreated, int recovery_treated)
        {
            this.susceptible = susceptible;
            this.infected_U = infected_U;
            this.infected_TR = 0;
            this.recovered = 0;
            this.vaccinated = 0;

            this.proportion_treated = 0;
            this.treatment_reduction = treatment_reduction;
            this.background_transmission_rate = background_transmission_rate;
            this.recovery_treated = recovery_treated;
            this.recovery_untreated = recovery_untreated;
        }

        public float change_in_susceptible()
        {
            return -1 * this.background_transmission_rate * this.susceptible * (this.infected_U + this.treatment_reduction * this.infected_TR);
        }

        public float getProportionTreated()
        {
            return this.proportion_treated;
        }

        public void setProportionTreated(float proportionTreated)
        {
            this.proportion_treated = proportionTreated;
        }

        public void setVaccination(float vaccinated_proportion)
        {
            this.proportion_vaccinated = vaccinated_proportion;
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
            // Apply vaccines
            this.susceptible = this.susceptible * (1 - this.proportion_vaccinated);
            this.vaccinated += this.susceptible * this.proportion_vaccinated;

            // Apply all other logic
            float s = this.change_in_susceptible();
            float i_u = this.change_in_infected_U();
            float i_tr = this.change_in_infected_TR();
            float r = this.change_in_recovered();

            this.time += 1;
            this.susceptible += s;
            this.infected_U += i_u;
            this.infected_TR += i_tr;
            this.recovered += r;
        }

        public void display()
        {
            Console.WriteLine("\nDay: " + this.time);
            Console.WriteLine("Sus: " + this.susceptible);
            Console.WriteLine("Inf_U: " + this.infected_U);
            Console.WriteLine("Inf_TR: " + this.infected_TR);
            Console.WriteLine("Recov: " + this.recovered);
            Console.WriteLine("Vaccin: " + this.vaccinated);
        }


        static int Main(string[] args)
        {
            SIRModel model = new SIRModel(500000, 1, (float)0.5, (float)0.000001, 14, 7);
            model.display();
            for (int i = 0; i < 50; i++)
            {
                if (i == 20)
                {
                    model.setProportionTreated((float)0.3);
                    model.setVaccination(0.0001f);
                }
                model.update();
                model.display();
            }

            Console.WriteLine("Enter to exit...");
            Console.ReadLine();
            return 0;
        }
    }
}

