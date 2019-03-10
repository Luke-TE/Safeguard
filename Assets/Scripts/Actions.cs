using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace healthHack
{

    public class Actions : MonoBehaviour
    {

        public Text t;
        string lastAction;
        int percentage;
        public Board board;
        public Text percentageVac;
        Transform cityChange;

        public void setPercentage(int Percentage)
        {
            percentage = Percentage;
        }

        public void Action(string Action)
        {
            cityChange = board.currentCityTrans;
            string val = percentageVac.text;
            setPercentage(Int32.Parse(val));    
            Complete_Action(Action);
        }

        private void Update()
        {

            t.text = lastAction + (cityChange.gameObject.GetComponent("City") as City).name + " by" + percentage + "%" ;
        }


        public void Complete_Action(string currentAction)
        {
            lastAction = currentAction;
            switch (currentAction)
            {
                case "Delete Path":
                    break;
                case "Vaccinate City":
                    (cityChange.gameObject.GetComponent("City") as City).getModel().setVaccine(percentage / 100);
                    break;
                case "Isolate City":
                    break;
                case "Introduce Drug Treatments":
                    (cityChange.gameObject.GetComponent("City") as City).getModel().setDrugTreatment(percentage / 100);
                    break;

            }

        }
    }
}
