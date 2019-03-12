
using System;
using UnityEngine;

namespace healthHack
{
    public class SIRModel : IModel
    {
        private const float _spreadMultiplier = 0.0000001f;

        //Costs                
        private float totalCost;
        private float cost;

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
        private float recoveryUntreated;
        private float recoveryTreated;

        public SIRModel(float popSusceptible, float popInfectedU, float treatmentMultiplier, int recoveryUntreated, int recoveryTreated)
        {            
            this.popSusceptible = popSusceptible;
            this.popInfectedU = popInfectedU;
            popInfectedTR = 0;
            popRecovered = 0;
            popVaccinated = 0;

            totalPop = popSusceptible + popInfectedU;

            totalCost = 0;
            cost = 0;

            proportionTreated = 0;
            this.treatmentMultiplier = treatmentMultiplier;

            backgroundTransmissionRate = _spreadMultiplier * Settings.GetDiseaseCoeff();
            this.recoveryTreated = recoveryTreated;
            this.recoveryUntreated = recoveryUntreated;
        }

        public void Update()
        {
            cost = 0;
            
            //Vaccination
            float vaccinatedThisTurn = popSusceptible * proportionVaccinated;
            
            popSusceptible -= popSusceptible * proportionVaccinated;
            cost += vaccinatedThisTurn * Settings._vaccineCostPP;
            popVaccinated += vaccinatedThisTurn;
                        
            //Medication
            cost += backgroundTransmissionRate * popSusceptible * 
                (popInfectedU + treatmentMultiplier * popInfectedTR) * proportionTreated * Settings._drugCostPP;
            
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
            return backgroundTransmissionRate * popSusceptible * (popInfectedU + treatmentMultiplier * popInfectedTR) * (1 - proportionTreated) - (1 / recoveryUntreated) * popInfectedU;
        }

        public float CalculateInfectedTRChange()
        {
            return backgroundTransmissionRate * popSusceptible * (popInfectedU + treatmentMultiplier * popInfectedTR) * proportionTreated - (1 / recoveryTreated) * popInfectedTR;
        }

        public float CalculateRecoveredChange()
        {
            return (1 / recoveryTreated) * popInfectedTR + (1 / recoveryUntreated) * popInfectedU;
        }
        
        public void Display()
        {
            Console.Write(ToString());
        }

        public override string ToString()
        {
            string text = "\n";
            text += "Day:  " + Timer.DaysPassed + "\n";
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

        public void InfectPopulation(float quantity)
        {
            float infect = Math.Min(quantity, popSusceptible);

            popInfectedU += infect;
            popSusceptible -= infect;
        }

        public float GetTotalPopulation()
        {
            return totalPop;
        }

        public float GetLastCost()
        {
            return totalCost;
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
