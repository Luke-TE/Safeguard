
using System;
using UnityEngine;

namespace healthHack
{
    public class SIRModel : IModelInterface
    {
        //Costs                
        private float totalCost = 0;

        //Population
        private float totalPop;
        private float popSusceptible;
        private float popInfectedU;
        private float popInfectedTR;
        private float popRecovered;
        private float popVaccinated;
                
        private float proportionTreated;
        private float proportionVaccinated;

        private float treatmentMultiplier;
        private float backgroundTransmissionRate;
        private float recovery_untreated;
        private float recovery_treated;

        public SIRModel(float popSusceptible, float popInfectedU, float treatmentMultiplier, int recovery_untreated, int recovery_treated)
        {            
            this.popSusceptible = popSusceptible;
            this.popInfectedU = popInfectedU;
            popInfectedTR = 0;
            popRecovered = 0;
            popVaccinated = 0;

            totalPop = popSusceptible + popInfectedU;            
            
            proportionTreated = 0;
            this.treatmentMultiplier = treatmentMultiplier;

            backgroundTransmissionRate = 0.0000001f * Settings.GetDiseaseCoeff();
            this.recovery_treated = recovery_treated;
            this.recovery_untreated = recovery_untreated;
        }

        public void Update()
        {
            float cost = 0;
            
            //Vaccination
            float vaccinatedThisTurn = popSusceptible * proportionVaccinated;
            popSusceptible -= popSusceptible * proportionVaccinated;
            cost += vaccinatedThisTurn * Settings._vaccineCostPP;
            popVaccinated += vaccinatedThisTurn;
                        
            //Medication
            cost += backgroundTransmissionRate * popSusceptible * 
                (popInfectedU + treatmentMultiplier * popInfectedTR) * GetProportionTreated() * Settings._drugCostPP;
            
            popSusceptible -= CalculateSusceptibleDecrease();
            popInfectedU += CalculateInfectedUChange();
            popInfectedTR += CalculateInfectedTRChange();
            popRecovered += CalculateRecoveredChange();
            totalCost += cost;
            Display();
        }

        public float CalculateSusceptibleDecrease()
        {
            return backgroundTransmissionRate * popSusceptible * (popInfectedU + treatmentMultiplier * popInfectedTR);
        }

        public float GetProportionTreated()
        {
            return proportionTreated;
        }        

        public bool SetProportionTreated(float proportionTreated)
        {
            if (0 <= proportionTreated && proportionTreated <= 1)
            {
                this.proportionTreated = proportionTreated;
                return true;
            }
            return false;
        }

        public bool SetProportionVaccinated(float proportionVaccinated)
        {
            if (0 <= proportionVaccinated && proportionVaccinated <= 1)
            {
                this.proportionVaccinated = proportionVaccinated;
                
                return true;
            }
            return false;
        }

        public float CalculateInfectedUChange()
        {
            return this.backgroundTransmissionRate * this.popSusceptible * (this.popInfectedU + this.treatmentMultiplier * this.popInfectedTR) * (1 - this.GetProportionTreated()) - (1 / this.recovery_untreated) * this.popInfectedU;
        }

        public float CalculateInfectedTRChange()
        {
            return this.backgroundTransmissionRate * this.popSusceptible * (this.popInfectedU + this.treatmentMultiplier * this.popInfectedTR) * this.GetProportionTreated() - (1 / this.recovery_treated) * this.popInfectedTR;
        }

        public float CalculateRecoveredChange()
        {
            return (1 / this.recovery_treated) * this.popInfectedTR + (1 / this.recovery_untreated) * this.popInfectedU;
        }

        

        public void Display()
        {
            Console.Write(ToString());
        }

        public override string ToString()
        {
            string text = "\n";
            text += "Day:  " + Timer.TimePassed + "\n";
            text += "Sus: " + popSusceptible + "\n";
            text += "Inf_U: " + popInfectedU + "\n";
            text += "Inf_TR: " + popInfectedTR + "\n";
            text += "Recov: " + popRecovered + "\n";
            text += "Vaccin: " + popVaccinated + "\n";            
            text += "TOTAL COSTS: " + totalCost + "\n";
            return text;
        }        


        public bool AddAntiVaxxer()
        {
            throw new NotImplementedException();
        }
        
        public float GetTotalCost()
        {
            return totalCost;
        }

        public float GetInfectedPopulation()
        {
            return popInfectedTR + popInfectedU;
        }

        public float GetSusceptiblePopulation()
        {
            return popSusceptible;
        }

        public float GetDead()
        {
            throw new NotImplementedException();
        }

        public float GetRecovered()
        {
            return popRecovered + popVaccinated;
        }

        public float GetLastCost()
        {
            return cost;
        }

        public void ExternalInfect(float quantity)
        {
            float infect = Math.Min(quantity, popSusceptible);

            popInfectedU += infect;
            popSusceptible -= infect;
        }

        public float GetTotalPopulation()
        {
            return totalPop;
        }

        static int Test()
        {
            SIRModel model = new SIRModel(500000, 1, 0.5f, 14, 7);
            model.Display();
            for (int i = 0; i < 50; i++)
            {
                if (i == 20)
                {
                    model.SetProportionTreated(0.3f);
                    model.SetProportionVaccinated(0.0001f);
                }
                model.Update();
                model.Display();
            }

            Console.WriteLine("Enter to exit...");
            Console.ReadLine();
            return 0;
        }
    }
}
