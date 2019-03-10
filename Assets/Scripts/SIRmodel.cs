
using System;

namespace healthHack
{
    public class SIRModel : IModelInterface
    {
        private float COST_OF_DRUG;
        private float COST_OF_VACCINE;


        private float MAX_POPULATION;
        private float susceptible;
        private float infected_U;
        private float infected_TR;
        private float recovered;
        private float vaccinated;



        private float cost = 0;
        private float total_cost = 0;
        private int time;
        private float proportion_treated;
        private float proportion_vaccinated;

        private float treatment_reduction;
        private float background_transmission_rate;
        private float recovery_untreated;
        private float recovery_treated;

        public SIRModel(float susceptible, float infected_U, float treatment_reduction, float background_transmission_rate, int recovery_untreated, int recovery_treated)
        {
            this.COST_OF_DRUG = 5;
            this.COST_OF_VACCINE = 15;

            this.susceptible = susceptible;
            this.infected_U = infected_U;
            this.MAX_POPULATION = susceptible + infected_U;
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

        public bool setDrugTreatment(float proportionTreated)
        {
            if (0 <= proportionTreated && proportionTreated <= 1)
            {
                this.proportion_treated = proportionTreated;
                return true;
            }
            return false;
        }

        public bool setVaccine(float vaccinated_proportion)
        {
            if (0 <= vaccinated_proportion && vaccinated_proportion <= 1)
            {
                this.proportion_vaccinated = vaccinated_proportion;
                return true;
            }
            return false;
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

        public void Update()
        {
            this.cost = 0;
            // Apply vaccines
            this.susceptible = this.susceptible * (1 - this.proportion_vaccinated);
            float v = this.susceptible * this.proportion_vaccinated;

            this.vaccinated += v;
            this.cost += v * this.COST_OF_VACCINE;

            // Apply all other logic
            float s = this.change_in_susceptible();
            float i_u = this.change_in_infected_U();
            float i_tr = this.change_in_infected_TR();
            float r = this.change_in_recovered();

            this.cost += this.background_transmission_rate * this.susceptible * (this.infected_U + this.treatment_reduction * this.infected_TR) * this.getProportionTreated() * this.COST_OF_DRUG;

            this.time += 1;
            this.susceptible += s;
            this.infected_U += i_u;
            this.infected_TR += i_tr;
            this.recovered += r;
            this.total_cost += cost;
            this.display();
        }

        public void display()
        {
            Console.WriteLine("\nDay: " + this.time);
            Console.WriteLine("Sus: " + this.susceptible);
            Console.WriteLine("Inf_U: " + this.infected_U);
            Console.WriteLine("Inf_TR: " + this.infected_TR);
            Console.WriteLine("Recov: " + this.recovered);
            Console.WriteLine("Vaccin: " + this.vaccinated);
            Console.WriteLine("Costs: " + this.cost);
            Console.WriteLine("TOTAL COSTS: " + this.total_cost);
        }


        static int Main(string[] args)
        {
            SIRModel model = new SIRModel(500000, 1, (float)0.5, (float)0.000001, 14, 7);
            model.display();
            for (int i = 0; i < 50; i++)
            {
                if (i == 20)
                {
                    model.setDrugTreatment((float)0.3);
                    model.setVaccine(0.0001f);
                }
                model.Update();
                model.display();
            }

            Console.WriteLine("Enter to exit...");
            Console.ReadLine();
            return 0;
        }

        public bool AddAntiVaxxer()
        {
            return true;
        }

        public float GetCosts()
        {
            return this.cost;
        }

        public float GetTotalCost()
        {
            return this.total_cost;
        }

        public float GetInfected()
        {
            return this.infected_TR + this.infected_U;
        }

        public float GetSusceptible()
        {
            return this.susceptible;
        }

        public float GetDead()
        {
            throw new NotImplementedException();
        }

        public float GetRecovered()
        {
            return this.recovered;
        }

        public float GetLastCost()
        {
            return this.total_cost;
        }

        public void ExternalInfect(float quantity)
        {
            float infect = Math.Min(quantity, this.susceptible);

            this.infected_U += infect;
            this.susceptible -= infect;
        }

        public float GetTotalPopulation()
        {
            return this.MAX_POPULATION;
        }
    }
}
