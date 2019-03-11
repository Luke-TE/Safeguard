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
        string lastAction = "";
        int percentage = 0;
        private string name = "";
        private bool clicked;
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
            name  = (cityChange.gameObject.GetComponent("City") as City).name;
            clicked = true;
        }

        private void Update()
        {
            if (clicked)
            {
                t.text = lastAction + ": " + (name + " by " + percentage + "%");
            }
        }


        public void Complete_Action(string currentAction)
        {
            lastAction = currentAction;
            switch (currentAction)
            {
                case "Delete Path":
                    break;
                case "Vaccinate City":
                    (cityChange.gameObject.GetComponent("City") as City).getModel().SetProportionVaccinated((float)percentage / 100);
                    Debug.Log("Vaccinate");
                    break;
                case "Isolate City":
                    break;
                case "Introduce Drug Treatments":
                    (cityChange.gameObject.GetComponent("City") as City).getModel().SetProportionTreated((float)percentage / 100);
                    break;

            }

        }
    }
}
